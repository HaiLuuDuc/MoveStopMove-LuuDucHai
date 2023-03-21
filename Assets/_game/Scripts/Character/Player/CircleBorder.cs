using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBorder : MonoBehaviour
{
    [SerializeField] private Transform characterTransform;
    void Update()
    {
        transform.position = characterTransform.position;
        /*Debug.Log("Screen Point of circle border: " + Camera.main.WorldToScreenPoint(transform.position));
        Debug.Log("Mouse position: " + Input.mousePosition);*/

    }
}
