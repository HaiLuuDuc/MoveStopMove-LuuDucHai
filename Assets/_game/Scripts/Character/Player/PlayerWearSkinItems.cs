using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWearSkinItems : CharacterWearSkinItems
{
    private GameObject currentHat;
    private GameObject currentShield;

    public override GameObject WearHat(int index)
    {
        DestroyCurrentHat();
        currentHat = base.WearHat(index);
        return null;
    }

    public void DestroyCurrentHat()
    {
        if (currentHat != null)
        {
            Destroy(currentHat);
        }
    }

    public override void WearPants(int index)
    {
        pants.material = SkinShopManager.instance.pants[index];
    }

    public void DestroyCurrentPants()
    {
        pants.material = Colors.instance.transparent100;
    }

    public override GameObject WearShield(int index)
    {
        DestroyCurrentShield();
        currentShield = base.WearShield(index);
        return null;
    }

    public void DestroyCurrentShield()
    {
        if (currentShield != null)
        {
            Destroy(currentShield);
        }
    }

    /*public void WearFullSet(int index)
    {

    }*/

}
