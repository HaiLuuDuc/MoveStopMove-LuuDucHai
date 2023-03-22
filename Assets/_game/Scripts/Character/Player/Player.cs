using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private CharacterAnimation characterAnimation;


    protected override void Start()
    {
        base.Start();
        OnInit();
    }

    public override void OnInit() {
        base.OnInit();
        isMoving = false;
        isDead = false;
        enemyList.Clear();
        transform.position = new Vector3(30.1900005f, 56.0499992f, -8.63000011f);
        characterAnimation.ChangeAnim(Constant.IDLE);
        skinnedMeshRenderer.material = whiteMaterial;
    }

    public void OnDeath() {
        characterAnimation.ChangeAnim(Constant.DIE);
        isDead = true;
        skinnedMeshRenderer.material = blackMaterial;
        LevelManager.instance.DeleteThisElementInEnemyLists(this);
        LevelManager.instance.currentAlive--;
        LevelManager.instance.characterList.Remove(this);
        UIManager.instance.ShowLosePanel();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.WEAPON) && other.GetComponent<Weapon>().owner != this)
        {
            OnDeath();
        }
    }
}
