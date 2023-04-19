using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    [Header("Position:")]
    public Transform topLeftCorner;
    public Transform bottomRightCorner;
    [SerializeField] private float spawnDistance;
    public float initialY;

    [Header("Manager:")]
    [SerializeField] private Transform bots;
    public BotPool botPool;
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

    public void Spawn()
    {
        //spawn pos and rot
        Vector3 spawnPosition;
        Vector3 spawnRotation;
        spawnRotation = new Vector3(0,Random.Range(0,360),0);
        do
        {
            int randomX = (int)Random.Range(topLeftCorner.position.x, bottomRightCorner.position.x);
            int randomZ = (int)Random.Range(bottomRightCorner.position.z, topLeftCorner.position.z);
            spawnPosition = new Vector3(randomX,initialY,randomZ);
        } while (CheckPosition(spawnPosition)==false); //spawn position cho bot sao cho nó không đứng gần các thằng khác
        Bot pooledBot = botPool.GetObject().GetComponent<Bot>();
        pooledBot.transform.position = spawnPosition;
        pooledBot.transform.rotation = Quaternion.Euler(spawnRotation);
        pooledBot.OnInit();
        pooledBot.transform.SetParent(bots);
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
        pooledBotName.GetComponent<CanvasNameOnUI>().SetColor(pooledBot);
        pooledBot.botName = pooledBotName;
        pooledBotName.SetActive(true);
        //spawn indicator
        pooledBot.target.SetColor(pooledBot);

        //spawn pants
        int index = (int)Random.Range(0, SkinShopManager.instance.pants.Length);
        pooledBot.botWearSkinItems.WearPants(index);
        //spawn head
        if (pooledBot.isHaveHat == false)
        {
            /*GameObject newHat = Instantiate(SkinShopManager.instance.hats[(int)Random.Range(0, SkinShopManager.instance.hats.Length)]);
            Quaternion hatLocalRotation = newHat.transform.rotation;
            newHat.transform.SetParent(pooledBot.dinhdau);
            newHat.transform.localPosition = Vector3.zero;
            newHat.transform.localRotation = hatLocalRotation;*/
            index = (int)Random.Range(0, SkinShopManager.instance.hats.Length);
            pooledBot.botWearSkinItems.WearHat(index);
            pooledBot.isHaveHat = true;
        }
        //spawn weapon
        if(pooledBot.isHaveWeapon == false)
        {
            GameObject newWeaponObj = Instantiate(WeaponShopManager.Instance.weapons[(int)Random.Range(0, WeaponShopManager.Instance.weapons.Length)].gameObject);
            Weapon newWeapon = newWeaponObj.GetComponent<Weapon>();
            newWeapon.gameObject.SetActive(true);
            newWeapon.transform.SetParent(pooledBot.rightHand.transform);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
            newWeapon.SetOwnerAndWeaponPool(pooledBot, pooledBot.weaponPool);
            pooledBot.onHandWeapon = newWeapon.gameObject;
            pooledBot.weaponPool.prefab = newWeapon.gameObject;
            pooledBot.onHandWeapon.GetComponent<BoxCollider>().enabled = false;
            pooledBot.isHaveWeapon = true;
        }

    }

    public void Despawn(Bot bot)
    {
        bot.DeActiveNavmeshAgent();
        BotNamePool.instance.ReturnToPool(bot.botName);// despawn pooledbotname
        botPool.ReturnToPool(bot.gameObject);
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
