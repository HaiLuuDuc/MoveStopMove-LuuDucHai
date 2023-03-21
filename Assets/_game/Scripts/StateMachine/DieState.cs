using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class DieState : IState
{
    float duration;
    float elasedTime;
    public void OnEnter(Bot bot)
    {
        elasedTime = 0f;
        duration = 2f;
        bot.StopMoving();
        bot.OnDeath(); // in OnDeath, alive--
    }
    public void OnExecute(Bot bot)
    {
        if(elasedTime<duration)
        {
            elasedTime += Time.deltaTime;
        }
        else
        {
            BotManager.instance.Despawn(bot);
            LevelManager.instance.currentAlive--;
            if (LevelManager.instance.currentAlive > BotManager.instance.size)
            {
                BotManager.instance.Spawn();
            }
        }
    }

    public void OnExit(Bot bot) { }
}
