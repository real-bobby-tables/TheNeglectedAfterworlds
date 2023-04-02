using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    public float Damage = 10.0f;
    public float Speed = 10.0f;

    public bool isHoming = false;
}
