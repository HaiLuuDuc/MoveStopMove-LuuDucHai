using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PantsController : ItemController
{
    public override void OnButtonClick(int index)
    {
        //mac do cho player
        player.WearPants(index);
        //hien thi buy button
        Item item = buttons[index].GetComponent<Item>();
        if (item.isPurchased == false)
        {
            buyButtonText.text = item.cost.ToString();
        }
        else
        {
            if (usingIndex == index)
            {
                buyButtonText.text = Constant.USING;
            }
            else
            {
                buyButtonText.text = Constant.USE;
            }
        }
        currentIndex = index;
    }
}
