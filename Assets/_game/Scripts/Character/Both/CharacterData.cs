using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    [SerializeField] private Material[] skinHeads;
    [SerializeField] private Material[] skinShorts;
    [SerializeField] private Material[] skinShields;
    [SerializeField] private Material[] skinOutfits;

    [SerializeField] private Weapon[] weapons;

    public Weapon getWeapon(int index)
    {
        return weapons[index];
    }
}
