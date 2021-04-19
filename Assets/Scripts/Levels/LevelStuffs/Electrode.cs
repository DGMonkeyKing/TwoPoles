using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrode : MonoBehaviour
{
    private bool onDying = false;
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Electrode");
        onDying = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController pc = collider.gameObject.GetComponent<PlayerController>();

        if(pc != null)
        {
            if(!onDying) 
            {
                Transitions.MakeTransition(Transitions.TransitionType.ELECTRIC_DEATH);
                onDying = true;
            }
            pc.Destruction();
        }
    }
}
