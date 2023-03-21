using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PatrolState : IState
{
    float elapsedTime;
    float duration;
    float randomRotateValue;

    public void OnEnter(Bot bot)
    {
        elapsedTime = 0f;
        duration = Random.Range(4f, 7f);
        randomRotateValue = Random.Range(0.3f,0.4f);
        bot.Move();
        bot.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(-60f, 60f), 0));
    }

    public void OnExecute(Bot bot)
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            bot.navMeshAgent.SetDestination(bot.destinationTransform.position);
            bot.transform.Rotate(0, randomRotateValue, 0);
            if (bot.enemy != null && elapsedTime > 0.8f) // bot only attacks after 0.8 seconds patrol
            {
                bot.ChangeState(new AttackState());
            }
        }
        else
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot) { }
}
