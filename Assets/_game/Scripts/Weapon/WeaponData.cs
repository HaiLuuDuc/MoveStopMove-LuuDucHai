using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Knife = 0,
    Hammer = 1,
    Boomerang = 2,
}
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public WeaponType weaponType;
    public Sprite icon;
    public Mesh mesh;
    public Material[] weaponMaterials;
    public Material GetWeaponMaterial(int type)
    {
        return weaponMaterials[(int)type];
    }
}
