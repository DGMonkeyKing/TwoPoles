using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoChip : PickUpItem
{
    void OnEnable()
    {
    /*    if(!GlobalVariables.COLLECTABLE_INFO_CHIP_)
        {
            gameObject.SetActive(false);
        }*/
    }

    public override void EffectPickUp()
    {
        Debug.Log("Effecting...");
        //PlayerPrefs save Collectable item.
        Disappear();
    }

    public override void Disappear()
    {
        Destroy(gameObject);
    }
}
