using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected CharacterData characterData;
    [SerializeField] protected GameObject body;
    [SerializeField] protected Material blackMaterial;
    [SerializeField] protected Material whiteMaterial;
    [SerializeField] protected SkinnedMeshRenderer skinnedMeshRenderer;
    private Vector3 characterScaleOffset;
    private Vector3 weaponScaleOffset;
    private float scaleOffset;
    public GameObject onHandWeapon;
    public bool isMoving = false;
    public bool isDead = false;
    public List<Character> enemyList = new List<Character>();
    public List<Weapon> pooledWeaponList = new List<Weapon>();

    protected virtual void Start()
    {
        OnInit();
    }
    protected virtual void Update()
    {
        
    }
    public virtual void OnInit()
    {
        scaleOffset = 0.2f;
        characterScaleOffset = this.transform.localScale * scaleOffset; // 
        weaponScaleOffset = onHandWeapon.transform.lossyScale * scaleOffset; // (.5,.5,.5) = (.5,.5,.5) * 0.2 = (.1,.1,.1)
    }
    public void ChangeShort()
    {
        
    }
    public void ChangeWeapon()
    {
        
    }
    public void TurnBigger()
    {
        body.transform.localScale = (body.transform.localScale) + characterScaleOffset; ;
        Vector3 newScale = (onHandWeapon.transform.lossyScale) + weaponScaleOffset; //weaponScaleOffset = (0.1,0.1,0.1)
        // newScale = (0.5,0.5,0.5) + (0.1,0.1,0.1) = (0.6,0.6,0.6)
        onHandWeapon.transform.localScale = Vector3.Scale(onHandWeapon.transform.localScale,newScale); // needtofix: ko chi onhandweapon ma cac pooledWeapon cung to ra
        for(int i=0;i<pooledWeaponList.Count;i++)
        {
            pooledWeaponList[i].transform.localScale = Vector3.Scale(pooledWeaponList[i].transform.localScale, newScale);
        }
    }

    
}
