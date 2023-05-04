using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Weapon Object")]
public class WeaponObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { 
        get { return prefab;}
        private set {prefab = value; }
    }

    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value;}
    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value;}
    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value;}
    [SerializeField]
    int pierce; 
    public int Pierce { get => pierce; private set => pierce = value;}

    [SerializeField]
    int level;
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }
}
