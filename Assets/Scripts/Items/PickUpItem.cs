using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class PickUpItem : MonoBehaviour
{
    protected PlayerController pc;

    protected BoxCollider2D collider;

    void Awake()
    {
        collider = this.GetComponent<BoxCollider2D>();

        collider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        pc = collider.gameObject.GetComponent<PlayerController>();

        if(pc != null)
        {
            StartCoroutine("EffectPickUp");
        }
    }

    public abstract void EffectPickUp();
}
