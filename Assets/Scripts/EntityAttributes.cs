using UnityEngine;

[CreateAssetMenu(menuName = "Entity Attributes")]
public class EntityAttributes : ScriptableObject
{
    [SerializeField]
    float maxHealth;
    public float MaxHealth {get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float speed;
    public float Speed {get => speed; private set => speed = value; }
    [SerializeField]
    float damage;
    public float Damage {get => damage; private set => damage = value; }
}
