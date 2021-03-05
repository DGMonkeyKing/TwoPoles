using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Transitions
{
    public enum TransitionType
    {
        ELECTRIC_DEATH
    }

    // Start is called before the first frame update
    public static void MakeTransition(TransitionType type)
    {
        Debug.Log("Make Transition");
        GameObject go = GameObject.FindGameObjectsWithTag("Transitions")[0];
        AbstractTransition at;
        
        switch(type)
        {
            case TransitionType.ELECTRIC_DEATH:
                at = (ElectricDeathTransition) go.GetComponentInChildren<ElectricDeathTransition>();
            break;
            default:
            break;
        }
    }
}
