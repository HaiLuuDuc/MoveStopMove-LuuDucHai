using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTabSkinShop : BaseButton
{
    [SerializeField] private GameObject area;
    [SerializeField] private GameObject buyButton;

    protected override void OnClick()
    {
        UIManager.instance.HideAllChooseAreas();
        SkinShopManager.instance.CloseAllBuyButtons();
        ShowArea();
        ShowBuyButton();
    }

    

    public void ShowArea()
    {
        area.SetActive(true);
    }

    public void ShowBuyButton()
    {
        buyButton.SetActive(true);
    }
}
