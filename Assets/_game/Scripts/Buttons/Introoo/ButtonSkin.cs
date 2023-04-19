using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSkin : BaseButton
{
    protected override void OnClick()
    {
        CameraController.instance.StartCoroutine(CameraController.instance.SwitchTo(CameraState.Skin));
    }

/*    private void PutCurrentItemsOnPlayer()
    {
        ItemController ic;
        switch (itemType)
        {
            case (ItemType.Hat):
                ic = SkinShopManager.instance.itemControllers[(int)ItemType.Hat];
                player.WearHat(ic.currentIndex);
                break;
            case (ItemType.Pants):
                ic = SkinShopManager.instance.itemControllers[(int)ItemType.Hat];
                player.WearPants(ic.currentIndex);
                break;
            case (ItemType.Shield):
                ic = SkinShopManager.instance.itemControllers[(int)ItemType.Hat];
                player.WearShield(ic.currentIndex);
                break;

        }
    }*/
}
