using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public List<Character> characterList = new List<Character>();
    public int initialAlive;
    public int currentAlive;
    public bool isGaming;

    //singleton
    public static LevelManager instance;
    private void Awake()
    {
        instance = this;
        characterList.Add(player);
    }

    void Start()
    {
        currentAlive = initialAlive;
        isGaming = false;
    }

    private void Update()
    {
        if(characterList.Count == 1 && player.isDead == false)
        {
            UIManager.instance.ShowWinPanel();
        }
    }

    public void RemakeLevel()
    {
        DeleteCharacters();
        RespawnCharacters();
        UIManager.instance.ShowJoystick();
        currentAlive = initialAlive;
        isGaming = false;
    }

    public void DeleteCharacters()
    {
        if (!player.isDead)
        {
            characterList.Remove(player);
        }
        while (characterList.Count>0)
        {
            // tat het cac weapon dang bay
            for (int j = 0; j < characterList[Constant.FIRST_INDEX].pooledWeaponList.Count; j++)
            {
                if (characterList[Constant.FIRST_INDEX].pooledWeaponList[j].gameObject.activeSelf)
                {
                    characterList[Constant.FIRST_INDEX].pooledWeaponList[j].gameObject.SetActive(false);
                }
            }
            // despawn bot
            BotManager.instance.Despawn(characterList[Constant.FIRST_INDEX].gameObject.GetComponent<Bot>());
        }
        characterList.Clear();
    }

    public void RespawnCharacters()
    {
        characterList.Add(player);
        player.OnInit();
        for (int i = 0; i < BotManager.instance.botList.Count; i++)
        {
            characterList.Add(BotManager.instance.botList[i]);
            BotManager.instance.Spawn();
        }
    }

    public void DeleteThisElementInEnemyLists(Character character)
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            if (characterList[i].enemyList.Contains(character))
            {
                characterList[i].enemyList.Remove(character);
            }
        }
    }
}
