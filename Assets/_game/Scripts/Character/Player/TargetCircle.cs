using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TargetCircle : MonoBehaviour
{

    [SerializeField] private float rotateSpeed;
    public Transform enemyTransform;
    void Update()
    {
        if(this.gameObject.activeSelf)
        {
            transform.Rotate(0, rotateSpeed, 0);
            if(enemyTransform != null)
            {
                transform.position = enemyTransform.position;
            }
        }
    }
}
