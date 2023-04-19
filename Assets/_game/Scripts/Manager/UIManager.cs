using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Joystick:")]
    [SerializeField] private Joystick joystick;
    [Header("Alive:")]
    [SerializeField] private Text aliveText;
    [SerializeField] private GameObject aliveTextObj;
    [Header("Panels:")]
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    [Header("Coins:")]
    [SerializeField] private TextMeshProUGUI coinText;
    [Header("Shop:")]
    [SerializeField] private GameObject weaponShop;
    [SerializeField] private GameObject hatArea;
    [SerializeField] private GameObject pantsArea;
    [SerializeField] private GameObject shieldArea;
    [SerializeField] private GameObject fullsetArea;
    [Header("Names and Indicators:")]
    [SerializeField] private GameObject indicators;
    [SerializeField] private GameObject CanvasName;


    //singleton
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HideJoystick();
        HideCanvasName();
        HideIndicators();
        HideAliveText();
    }

    private void Update()
    {
        aliveText.text = LevelManager.instance.currentAlive.ToString();
    }

    //close all
    public void CloseAll()
    {
        HideJoystick();
        HideLosePanel();
        HideWinPanel();
        HideWeaponShop();
        HideIndicators();
        HideCanvasName();
        HideAliveText();
    }
    
    //joystick
    public void ShowJoystick()
    {
        CloseAll();
        joystick.gameObject.SetActive(true);
        joystick.enabled = true;
    }

    public void HideJoystick()
    {
        joystick.enabled = false;
        joystick.gameObject.SetActive(false);
    }

    //lose panel
    public void ShowLosePanel()
    {
        CloseAll();
        losePanel.SetActive(true);
    }

    public void HideLosePanel()
    {
        losePanel.SetActive(false);
    }

    //win panel
    public void ShowWinPanel()
    {
        CloseAll();
        winPanel.SetActive(true);
    }

    public void HideWinPanel()
    {
        winPanel.SetActive(false);
    }

    //coin
    public void UpdateCoin()
    {
        coinText.text = LevelManager.instance.coin.ToString();
    }

/*    public void MoveMatBackground(Vector3 pos)
    {
        //matBackground.rectTransform = Vector3.zero;
    }*/

    //weapon shop
    public void ShowWeaponShop()
    {
        weaponShop.SetActive(true);
    }

    public void HideWeaponShop()
    {
        weaponShop.SetActive(false);
    }

    //indicators
    public void ShowIndicators()
    {
        indicators.SetActive(true);
    }

    public void HideIndicators()
    {
        indicators.SetActive(false);
    }

    //canvas name
    public void ShowCanvasName()
    {
        CanvasName.SetActive(true);
    }

    public void HideCanvasName()
    {
        CanvasName.SetActive(false);
    }

    //alive text
    public void ShowAliveText()
    {
        aliveTextObj.SetActive(true);
    }

    public void HideAliveText()
    {
        aliveTextObj.SetActive(false);
    }

    //choose areas
    public void HideAllChooseAreas()
    {
        hatArea.SetActive(false);
        pantsArea.SetActive(false);
        shieldArea.SetActive(false);
        fullsetArea.SetActive(false);
    }
}