using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Bot : Character, IChangeState, INavMeshAgent, ICheckDesOutOfMap
{
    [SerializeField] private Transform targetWeapon;
    private IState currentState = new IdleState();
    public BotWearSkinItems botWearSkinItems;
    public BotAttack botAttack;
    public IndicatorTarget target;
    public Transform destinationTransform;
    public NavMeshAgent navMeshAgent;
    public GameObject botName;
    public bool isHaveWeapon;

    protected void Update()
    {
        currentState.OnExecute(this);
        if (isDead == true)
        {
            return;
        }

    }

    public override void OnInit()
    {
        base.OnInit();
        ActiveNavmeshAgent();
        ChangeState(new IdleState());
        botAttack.enemy = null;
        target.enabled = true;
        skinnedMeshRenderer.material = Colors.instance.characterColors[(int)Random.Range(0, Colors.instance.characterColors.Length)];
    }

    public override void OnDeath()
    {
        DisableCollider();
        characterAnim.ChangeAnim(Constant.DIE);
        UnDisplayOnHandWeapon();
        skinnedMeshRenderer.material = deathMaterial;
        target.enabled = false; //turn off indicator
        LevelManager.instance.DeleteThisElementInEnemyLists(this);
    }

    public void StopMoving()
    {
        characterAnim.ChangeAnim(Constant.IDLE);
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped= true;
    }

    public void Move()
    {
        characterAnim.ChangeAnim(Constant.RUN);
        navMeshAgent.isStopped = false;
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

    public override void EnableCollider()
    {
        capsulCollider.enabled = true;
    }

    public override void DisableCollider()
    {
        capsulCollider.enabled = false;
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
        if (other.CompareTag(Constant.WEAPON) && other.GetComponent<Weapon>().GetOwner()!=this)
        {
            ChangeState(new DieState());
            isDead = true;
        }
    }
}
