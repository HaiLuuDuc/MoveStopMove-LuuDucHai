using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;

    //singleton
    public static WeaponManager instance;
    private void Awake()
    {
        instance = this;
    }
}
