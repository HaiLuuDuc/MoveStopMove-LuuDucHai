using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : MonoBehaviour
{
    public static WeaponPool instance;
    public GameObject prefab;
    public int poolSize = 5;
    public Character owner;
    private List<GameObject> pool = new List<GameObject>();
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            Weapon weapon = obj.GetComponent<Weapon>();
            weapon.weaponPool = this;
            weapon.owner = this.owner;
            owner.pooledWeaponList.Add(weapon);
            pool.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // If we get here, all objects are in use
        GameObject newObj = Instantiate(prefab);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.transform.localPosition = Vector3.zero;
        obj.SetActive(false);
    }
}