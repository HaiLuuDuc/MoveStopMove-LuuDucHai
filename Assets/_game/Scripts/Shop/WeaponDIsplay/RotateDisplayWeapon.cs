using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDisplayWeapon : MonoBehaviour
{
    [SerializeField] private float speedRotate;
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();

    void Update()
    {
        transform.Rotate(new Vector3(0,speedRotate,0));
    }
}
