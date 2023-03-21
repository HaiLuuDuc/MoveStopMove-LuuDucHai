using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;
    private void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(60,0,0));
    }
    private void LateUpdate()
    {
        /*if (Input.GetMouseButtonDown(0) && LevelManager.instance.isGaming == false)
        {
            LevelManager.instance.isGaming = true;
            StartCoroutine(RotateCamera(transform.rotation, Quaternion.Euler(new Vector3(60, 0, 0))));
        }*/
        transform.position = Vector3.Lerp(transform.position, target.position + offset, speed * Time.deltaTime);
        /*Debug.Log("scr to wrld" + Camera.main.ScreenToWorldPoint(transform.position));
        Debug.Log("wrld to scr" + Camera.main.WorldToScreenPoint(transform.position));*/


    }
    /*public IEnumerator RotateCamera(Quaternion currentRotation, Quaternion newRotation)
    {
        float duration = 0.5f;
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(currentRotation, newRotation,0.1f * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }*/
}
