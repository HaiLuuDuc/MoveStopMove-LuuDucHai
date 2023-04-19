using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemController : MonoBehaviour
{
    [Header("Player:")]
    public PlayerWearSkinItems player;
    [Header("Button:")]
    public Text buyButtonText;
    public Button[] buttons;
    [Header("Indexs:")]
    public int currentIndex = 0;
    public int usingIndex = -1;


    protected virtual void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int localIndex = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(localIndex));
        }
    }
    public abstract void OnButtonClick(int index);
}
