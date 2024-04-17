using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponItem : ScriptableObject
{
    public int durability;
    public Sprite sprite;
    public Color color;
    public int damage;
}
