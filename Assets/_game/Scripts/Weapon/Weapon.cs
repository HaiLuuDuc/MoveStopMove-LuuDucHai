using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    public WeaponPool weaponPool;
    public Character owner;
    //private MaterialType weaponMaterialType;
    //private int weaponStatus;
    private void Start()
    {

    }
    private void OnEnable()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.BOT)|| other.gameObject.CompareTag(Constant.PLAYER))
        {
            Character character = other.gameObject.GetComponent<Character>();
            if(character != this.owner){
                weaponPool.ReturnToPool(this.gameObject);
                //this.owner.TurnBigger();
            }
        }
    }
    /*public void ChangeWeaponMaterial(int material)    
    {
        Material[] materials = new Material[meshRenderer.materials.Length];
        for (int i = 0; i<materials.Length; i++)
        {
            materials[i] = null;
            materials[i] = weaponData.GetWeaponMaterial(material) ;
        }
        meshRenderer.materials = materials;
    }*/
    /*public void ChangeWeaponMaterial(MaterialType materialType)
    {
        weaponMaterialType = materialType;
        meshRenderer.material = weaponData.GetWeaponMaterial(weaponMaterialType);
    }*/

}

