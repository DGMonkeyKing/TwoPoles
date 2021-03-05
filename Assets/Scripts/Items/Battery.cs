using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : PickUpItem
{
    public override void EffectPickUp()
    {
        Debug.Log("Effecting...");
        pc.OnChargeAll.Invoke();
        Destroy(gameObject);
    }
}
