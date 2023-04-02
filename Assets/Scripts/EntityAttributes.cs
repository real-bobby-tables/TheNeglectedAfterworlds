using UnityEngine;

[CreateAssetMenu(menuName = "Entity Stats")]
public class EntityAttributes : ScriptableObject
{
    public float Health;
    public float Speed;
    public float MeleeDamage;
    public float ProjectileDamage;
    public float Defense;
}
