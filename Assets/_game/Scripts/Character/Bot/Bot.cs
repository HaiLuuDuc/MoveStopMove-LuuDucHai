using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private GameObject rightHand;
    [SerializeField] private Transform targetWeapon;
    [SerializeField] private WeaponPool weaponPool;
    [SerializeField] private CapsuleCollider capsulCollider;
    [SerializeField] private Target target;
    private IState currentState = new PatrolState();
    private float attackRange = 9f;
    private Vector3 enemyPosition;
    public Transform destinationTransform;
    public NavMeshAgent navMeshAgent;
    public CharacterAnimation characterAnimation;
    public Character enemy;
    public GameObject botName;


    protected override void Start()
    {
        OnInit();
    }
    protected override void Update()
    {
        currentState.OnExecute(this);
        if (isDead == true)
        {
            return;
        }
        if (enemyList.Count > 0)
        {
            FindNearestTarget();
        }
        else
        {
            enemy = null;
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        capsulCollider.enabled = true;
        onHandWeapon.SetActive(true);
        onHandWeapon.transform.SetParent(rightHand.transform);
        onHandWeapon.transform.localPosition = Vector3.zero;
        skinnedMeshRenderer.material = whiteMaterial;
        isDead = false;
        enemyList.Clear();
        ActiveNavmeshAgent();
        ChangeState(new PatrolState());
        target.enabled = true;
    }

    public void OnDeath()
    {
        capsulCollider.enabled = false;
        characterAnimation.ChangeAnim(Constant.DIE);
        onHandWeapon.SetActive(false);
        skinnedMeshRenderer.material = blackMaterial;
        target.enabled = false;
        LevelManager.instance.DeleteThisElementInEnemyLists(this);
    }

    public void Move()
    {
        //Debug.Log("ChangeAnim(Constant.RUN)");
        characterAnimation.ChangeAnim(Constant.RUN);
        navMeshAgent.isStopped = false;
    }
    
    public void StopMoving()
    {
        //Debug.Log("ChangeAnim(Constant.IDLE)");
        characterAnimation.ChangeAnim(Constant.IDLE);
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped= true;
    }

    public void FindNearestTarget()
    {
        enemy = null;
        if (enemyList.Count > 0)
        {
            float minDistance = 100f;
            for (int i = 0; i < enemyList.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, enemyList[i].transform.position);
                if (distance < minDistance)
                {
                    enemy = enemyList[i];
                    minDistance = distance;
                }
            }
        }
    }

    public void RotateToTarget()
    {
        if (enemy != null)
        {
            Vector3 dir;
            dir = enemy.transform.position - this.transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    public void ReCaculateTargetWeapon(GameObject obj)
    {
        Vector3 dir = enemyPosition - obj.transform.position;
        dir.y = 0;
        dir = dir.normalized;
        targetWeapon.transform.position = obj.transform.position + dir * attackRange;
    }

    public IEnumerator Attack()
    {
        characterAnimation.ChangeAnim(Constant.ATTACK);
        RotateToTarget();
        enemyPosition = enemy.transform.position; // luu vi tri cua enemy
        yield return new WaitForSeconds(0.4f);
        if (isDead)
        {
            yield break;
        }
        onHandWeapon.SetActive(false); // tat hien thi weapon tren tay
        GameObject obj = weaponPool.GetObject(); // lay weapon tu` pool
        obj.transform.position = rightHand.transform.position; // dat weapon vao tay phai character
        ReCaculateTargetWeapon(obj);// dam bao weapon bay qua center cua enemy
        StartCoroutine(FlyWeaponToTarget(obj, targetWeapon.transform.position, 8f)); // fly weapon
        yield return null;
    }

    public IEnumerator FlyWeaponToTarget(GameObject obj, Vector3 target, float speed)
    {
        while (Vector3.Distance(obj.transform.position, target) > 0.1f && obj.activeSelf)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, speed * Time.deltaTime);
            obj.transform.Rotate(0, speed, 0);
            yield return null;
        }
        WeaponPool.instance.ReturnToPool(obj); // sau khi bay den target thi cat vao pool
        yield return null;
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void ActiveNavmeshAgent()
    {
        navMeshAgent.enabled = true;
    }

    public void DeActiveNavmeshAgent()
    {
        navMeshAgent.enabled = false;
    }

    public bool CheckDestinationIsOutOfMap()
    {
        Vector3 pos = destinationTransform.position;
        if (!(
            pos.x > BotManager.instance.topLeftCorner.position.x &&
            pos.x < BotManager.instance.bottomRightCorner.position.x &&
            pos.z > BotManager.instance.bottomRightCorner.position.z &&
            pos.z < BotManager.instance.topLeftCorner.position.z
            ))
        {
            return true;
        }
        return false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isDead)
        {
            return;
        }
        if (other.CompareTag(Constant.WEAPON) && other.GetComponent<Weapon>().owner!=this)
        {
            ChangeState(new DieState());
            isDead = true;
        }
    }
}
