using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : MonoBehaviour
{
    [SerializeField] private Transform weaponClones;
    public GameObject prefab;
    public int poolSize = 5;
    public Character owner;
    public List<GameObject> pool = new List<GameObject>();

    //singleton
    public static WeaponPool instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newWeapon = Instantiate(prefab); // sinh ra 1 weapon moi
            newWeapon.SetActive(false);
            Weapon weapon = newWeapon.GetComponent<Weapon>();
            weapon.SetOwnerAndWeaponPool(this.owner, this);
            weapon.transform.localScale = Vector3.one;
            weapon.transform.localRotation = Quaternion.Euler(new Vector3(90,0,0));
            weapon.child.localRotation = Quaternion.Euler(Vector3.zero);
            weapon.GetComponent<BoxCollider>().enabled = true;
            newWeapon.transform.SetParent(weaponClones);
            owner.pooledWeaponList.Add(weapon);
            pool.Add(newWeapon);
        }
    }

    public void OnDestroy()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Destroy(pool[i]);
        }
        pool.Clear();
        owner.pooledWeaponList.Clear();
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
        obj.SetActive(false);
    }
}