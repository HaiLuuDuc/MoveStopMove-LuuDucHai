using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum ItemType
{
    Hat = 0, Pants = 1, Shield = 2, FullSet = 3
}
public class SkinShopManager : MonoBehaviour
{
    [Header("Player")]
    public PlayerWearSkinItems player;
    [Header("Items")]
    public GameObject[] hats;
    public Material[] pants;
    public GameObject[] shields;
    public GameObject[] fullSet;
    [Header("Item Controllers")]
    public ItemController[] itemControllers;
    [Header("Buy Buttons")]
    public GameObject[] buyButtons;

    //singleton
    public static SkinShopManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void BuyItem(ItemController itemController)
    {
        Item item = itemController.buttons[itemController.currentIndex].GetComponent<Item>();
        if (item.isPurchased == false && LevelManager.instance.coin >= item.cost)
        {
            item.isPurchased = true;
            LevelManager.instance.coin -= item.cost;
            UIManager.instance.UpdateCoin();
            itemController.buyButtonText.text = Constant.USING;
            itemController.usingIndex = itemController.currentIndex;
            UnlockSkin(item);
        }
        else if (item.isPurchased == true)
        {
            if (itemController.usingIndex != itemController.currentIndex)
            {
                itemController.buyButtonText.text = Constant.USING;
                itemController.usingIndex = itemController.currentIndex;
            }
            else
            {
                itemController.buyButtonText.text = Constant.USE;
                itemController.usingIndex = -1;
            }
        }
    }

    //close button
    public void CloseSkinShop()
    {
        PutItemsOnPlayer();
    }

    public void PutItemsOnPlayer()
    {
        //wear hat
        ItemController hatController = itemControllers[(int)ItemType.Hat];
        if (hatController.usingIndex >= 0)//nếu đã mua mũ rồi thì dùng cái mũ đó
        {
            player.WearHat(hatController.usingIndex);
        }
        else//nếu chưa mua cái nào mà chỉ đang thử thì sẽ phải trả lại mũ đang thử cho shop
        {
            player.DestroyCurrentHat();
        }
        //wear pants
        ItemController pantsController = itemControllers[(int)ItemType.Pants];
        if (pantsController.usingIndex >= 0)
        {
            player.WearPants(pantsController.usingIndex);
        }
        else
        {
            player.DestroyCurrentPants();
        }
        //wear shield
        ItemController shieldController = itemControllers[(int)ItemType.Shield];
        if (shieldController.usingIndex >= 0)
        {
            player.WearShield(shieldController.usingIndex);
        }
        else
        {
            player.DestroyCurrentShield();
        }
    }

    public void CloseAllBuyButtons()
    {
        for(int i = 0; i < buyButtons.Length; i++)
        {
            buyButtons[i].SetActive(false);
        }
    }

    public void UnlockSkin(Item skin)
    {
        skin.lockObj.SetActive(false);
    }
}
