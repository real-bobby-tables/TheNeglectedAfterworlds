using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Enemy;
    public GameObject[] Enemies;

    public int EnemyLimit = 20;
    public float SpawnDelay = 2.0f;

    private GameObject Player;
    private MainGameUI mui = null;
    public GameObject MainUIGameObject;
    private float horizontal_offset = 50.0f;
    private float vertical_offset = 50.0f;

    private List<GameObject> currentEnemies;

    private List<GameObject> playerMons; // this is the list that holds all the enemies the player can summon

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        currentEnemies = new List<GameObject>();
    }
    void Start()
    {
        if (MainUIGameObject != null)
        {
            mui = MainUIGameObject.GetComponent<MainGameUI>();
        }
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAllyMons()
    {
        for(int i = 0; i < playerMons.Count; i++)
        {
            playerMons[i].SetActive(true);
            playerMons[i].GetComponent<LifeSupport>().MonSpawn();
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float xDir = (Random.Range(0, 10) % 2 == 0 ? 1 : -1) * (horizontal_offset + Random.Range(1, 33));
        float yDir = (Random.Range(0, 10) % 2 == 0 ? 1 : -1) * (vertical_offset + Random.Range(1, 33));
        
        return new Vector3(xDir,yDir,0.0f) + Player.transform.position;
    }

    IEnumerator SpawnEnemy()
    {
        for (;;)
        {
            if (Enemy != null)
                {
                    if (currentEnemies.Count < EnemyLimit)
                    {
                        GameObject en = Instantiate(Enemy, GetRandomSpawnPosition(), Quaternion.identity);
                        AddEnemy(en);
                        yield return new WaitForSeconds(SpawnDelay);
                    }
                }
            yield return new WaitForSeconds(1.0f);
        }
    }

    public GameObject GetRandomEnemy()
    {
        if (currentEnemies.Count >= 1)
        {
            int rnd_idx = Random.Range(1, 999999) % currentEnemies.Count;
            return currentEnemies[rnd_idx] != null ? currentEnemies[rnd_idx] : null;
        }

        else {
            return null;
        }
        
    }

    public void AddMon(GameObject e)
    {
        playerMons.Add(e);
        if (mui != null)
        {
            mui.UpdateSoulCount(playerMons.Count);
        }
        
    }

    public void RemoveMon(GameObject e)
    {
        playerMons.Remove(e);
    }

    public bool HasEnemies()
    {
        return currentEnemies.Count > 0;
    }


    public void AddEnemy(GameObject e)
    {
        currentEnemies.Add(e);
    }

    public void RemoveEnemy(GameObject e)
    {
        currentEnemies.Remove(e);
    }
}
