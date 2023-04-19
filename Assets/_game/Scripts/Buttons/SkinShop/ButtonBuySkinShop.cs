using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBuySkinShop : BaseButton
{
    [SerializeField] private ItemController itemController;
    protected override void OnClick()
    {
        SkinShopManager.instance.BuyItem(itemController);
    }
}
