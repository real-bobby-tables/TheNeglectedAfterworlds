using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerController pc;
    public int chunkWidth = 21;


    [Header("Optimizations")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDistance; //must be greater than the length and width of the tilemap
    float opDist;

    float optimizerCooldown;
    public float optimizerCooldownDuration;



    // Start is called before the first frame update
    void Start()
    {
        pc = player.GetComponent<PlayerController>();
        optimizerCooldown = optimizerCooldownDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (pc != null)
        {
            CheckChunk();
            ChunkOptimizer();
        }

    }

    void CheckChunk()
    {
        if (!currentChunk)
        {
            return;
        }
        int xCoord = pc.MoveDir.x == 0 ? 0 : pc.MoveDir.x > 0 ? chunkWidth : -chunkWidth;
        int yCoord = pc.MoveDir.y == 0 ? 0 : pc.MoveDir.y > 0 ? chunkWidth : -chunkWidth;

        if (!Physics2D.OverlapCircle(currentChunk.transform.position + new Vector3(xCoord, yCoord, 0), checkRadius, terrainMask))
        {
            noTerrainPosition = currentChunk.transform.position + new Vector3(xCoord, yCoord, 0);
            SpawnChunk();
        }

        /*
        if(pc.LastMoveDir.x > 0 && pc.LastMoveDir.y == 0) // right
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
            }
        }

        else if(pc.LastMoveDir.x < 0 && pc.LastMoveDir.y == 0) // left
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
            }
        }
        else if(pc.LastMoveDir.x == 0 && pc.LastMoveDir.y > 0) // up
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                SpawnChunk();
            }
        }

        else if(pc.LastMoveDir.x == 0 && pc.LastMoveDir.y < 0) // down
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnChunk();
            }
        }

        else if(pc.LastMoveDir.x > 0 && pc.LastMoveDir.y > 0) // right up
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("RightUp").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("RightUp").position;
                SpawnChunk();
            }
        }

        else if(pc.LastMoveDir.x > 0 && pc.LastMoveDir.y < 0) // right down
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("RightDown").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("RightDown").position;
                SpawnChunk();
            }
        }

        else if(pc.LastMoveDir.x < 0 && pc.LastMoveDir.y > 0) // left up
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("LeftUp").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("LeftUp").position;
                SpawnChunk();
            }
        }

        else if(pc.LastMoveDir.x < 0 && pc.LastMoveDir.y < 0) // left down
        {
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("LeftDown").position, checkRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("LeftDown").position;
                SpawnChunk();
            }
        }
        */
    }


    void SpawnChunk()
    {
        int rnd = Random.Range(0, terrainChunks.Count);
        //Debug.Log("Spawning chunk at position: " + noTerrainPosition);
        latestChunk = Instantiate(terrainChunks[rnd], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;
        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDuration;
        }
        else {
            return;
        }
        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDist > maxOpDistance)
            {
                chunk.SetActive(false);
            }
            else 
            {
                chunk.SetActive(true);
            }
        }
    }

}
