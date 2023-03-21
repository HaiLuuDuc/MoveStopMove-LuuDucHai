using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Joystick joystick;
    [SerializeField] private Text aliveText;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;


    //singleton
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        aliveText.text = LevelManager.instance.currentAlive.ToString();
    }

    public void CloseAll()
    {
        HideJoystick();
        HideLosePanel();
        HideWinPanel();
    }

    public void ShowJoystick()
    {
        CloseAll();
        joystick.gameObject.SetActive(true);
        joystick.enabled = true;
    }

    public void HideJoystick() {
        joystick.enabled = false;
        joystick.gameObject.SetActive(false);
    }

    public void ShowLosePanel() {
        CloseAll();
        losePanel.SetActive(true);
    }

    public void HideLosePanel() {
        losePanel.SetActive(false);
    }

    public void ShowWinPanel()
    {
        CloseAll();
        winPanel.SetActive(true);
    }

    public void HideWinPanel()
    {
        winPanel.SetActive(false);
    }
}
