using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoChip : PickUpItem
{
    void OnEnable()
    {
        //if(!GameDataSingleton.COLLECTABLE_INFO_CHIP[InstanceID])
        //{
        //    gameObject.SetActive(false);
        //}
    }

    public override void EffectPickUp()
    {
        Debug.Log("Effecting...");
        //PlayerPrefs save Collectable item.
        Destroy(gameObject);
    }
}
