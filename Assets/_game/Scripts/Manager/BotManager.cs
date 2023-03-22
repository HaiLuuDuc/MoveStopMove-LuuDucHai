using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    public Transform topLeftCorner;
    public Transform bottomRightCorner;
    [SerializeField] private float spawnDistance;
    public BotPool botPool;
    public float initialY;
    public int size;
    public List<Bot> botList = new List<Bot>();
    
    //singleton
    public static BotManager instance;
    private void Awake()
    {
        instance= this;
    }

    void Start()
    {
        SpawnBots();
    }

    public void SpawnBots()
    {
        for(int i = 0; i < size; i++)
        {
            Spawn();
        }
    }

/*    public void DespawnBots()
    {
        for (int i = 0; i < size; i++)
        {
            Despawn();
        }
    }*/

    public void Spawn()
    {
        Vector3 spawnPosition;
        Vector3 spawnRotation;
        spawnRotation = new Vector3(0,Random.Range(0,360),0);
        do
        {
            int randomX = (int)Random.Range(topLeftCorner.position.x, bottomRightCorner.position.x);
            int randomZ = (int)Random.Range(topLeftCorner.position.z, bottomRightCorner.position.z);
            spawnPosition = new Vector3(randomX,initialY,randomZ);
        } while (CheckPosition(spawnPosition)==false);

        Bot pooledBot = botPool.GetObject().GetComponent<Bot>();
        pooledBot.transform.position = spawnPosition;
        pooledBot.transform.rotation = Quaternion.Euler(spawnRotation);
        pooledBot.OnInit();
        if (botList.Count<size)
        {
            botList.Add(pooledBot);
        }
        if (!LevelManager.instance.characterList.Contains(pooledBot))
        {
            LevelManager.instance.characterList.Add(pooledBot);
        }

        //spawn name
        GameObject pooledBotName = BotNamePool.instance.GetObject();
        pooledBotName.GetComponent<CanvasNameOnUI>().SetTargetTransform(pooledBot.transform);
        pooledBot.botName = pooledBotName;

    }

    public void Despawn(Bot bot)
    {
        bot.DeActiveNavmeshAgent();
        botPool.ReturnToPool(bot.gameObject);
        BotNamePool.instance.ReturnToPool(bot.botName);// despawn pooledbotname
        LevelManager.instance.characterList.Remove(bot);
    }

    public bool CheckPosition(Vector3 pos)
    {
        for(int i = 0; i < LevelManager.instance.characterList.Count; i++)
        {
            if (Vector3.Distance(LevelManager.instance.characterList[i].transform.position, pos) < spawnDistance)
            {
                return false;
            }
        }
        return true;
    }

   
}
