using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Rigidbody rb;

    protected void Start()
    {
        OnInit();
    }

    public override void OnInit() {
        base.OnInit();
        SetPosAndRot();
        StopMoving();
        GetWeaponFromInventory();
        characterAnim.ChangeAnim(Constant.IDLE);
        skinnedMeshRenderer.material = whiteMaterial;
        isMoving = false;
    }

    public override void OnDeath() {
        DisableCollider();
        characterAnim.ChangeAnim(Constant.DIE);
        isDead = true;
        skinnedMeshRenderer.material = deathMaterial;
        LevelManager.instance.DeleteThisElementInEnemyLists(this);
        LevelManager.instance.currentAlive--;
        LevelManager.instance.characterList.Remove(this);
        UIManager.instance.ShowLosePanel();
        //AudioManager.instance.Play(SoundType.Lose);
    }

    public override void EnableCollider()
    {
        capsulCollider.enabled = true;
        rb.velocity = Vector3.zero;
        rb.useGravity = true;
    }

    public override void DisableCollider()
    {
        capsulCollider.enabled = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    public void Idle()
    {
        characterAnim.ChangeAnim(Constant.IDLE);
    }

    public void Dance()
    {
        StopMoving();
        characterAnim.ChangeAnim(Constant.DANCE);
    }

    public void GetWeaponFromInventory()
    {
        GameObject wp = Instantiate(WeaponShopManager.Instance.GetWeapon());
        if (onHandWeapon != null)
        {
            Destroy(onHandWeapon);
        }
        onHandWeapon = wp;
        DisplayOnHandWeapon();
        onHandWeapon.transform.SetParent(rightHand.transform);
        onHandWeapon.transform.localPosition = Vector3.zero;
        onHandWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        onHandWeapon.GetComponent<BoxCollider>().enabled = false;
        wp.GetComponent<Weapon>().SetOwnerAndWeaponPool(this, this.weaponPool);
        weaponPool.prefab = wp;
        weaponPool.OnDestroy();
        weaponPool.OnInit();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.WEAPON) && other.GetComponent<Weapon>().GetOwner() != this)
        {
            OnDeath();
        }
    }
}
