using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrode : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController pc = collider.gameObject.GetComponent<PlayerController>();

        if(pc != null)
        {
            Transitions.MakeTransition(Transitions.TransitionType.ELECTRIC_DEATH);
            pc.Destruction();
        }
    }
}
