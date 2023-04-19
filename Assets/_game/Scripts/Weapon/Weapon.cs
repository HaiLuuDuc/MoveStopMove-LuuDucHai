using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform child;
    public MeshRenderer meshRenderer;
    private WeaponPool weaponPool;
    private Character owner;
    public int currentMaterialIndex;
    public bool isStuckAtObstacle;
    public bool isPurchased;
    //private MaterialType weaponMaterialType;
    //private int weaponStatus;
    private void Start()
    {
        currentMaterialIndex = 0;
        isStuckAtObstacle = false;
    }
    public void ChangeMaterial(int index)
    {
        meshRenderer.material = weaponData.GetWeaponMaterial(index);
    }
    public Character GetOwner()
    {
        return this.owner;
    }
    public void SetOwnerAndWeaponPool(Character owner, WeaponPool weaponPool)
    {
        this.owner = owner;
        this.weaponPool = weaponPool;
    }
    public IEnumerator ReturnToPoolAfterSeconds()
    {
        yield return new WaitForSeconds(1);
        weaponPool.ReturnToPool(this.gameObject);
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.BOT)|| other.gameObject.CompareTag(Constant.PLAYER))
        {
            Character character = other.gameObject.GetComponent<Character>();
            if(character != this.owner){
                weaponPool.ReturnToPool(this.gameObject);
                this.owner.TurnBigger();
                //play sound
                /*if (AudioManager.instance.IsInDistance(this.transform))
                {
                    AudioManager.instance.Play(SoundType.Die);
                }*/
            }
            if(character is Bot && this.owner is Player)
            {
                LevelManager.instance.coin += 10;
                UIManager.instance.UpdateCoin();
            }
        }

    }
  
}

