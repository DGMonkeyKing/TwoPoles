using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTransition : MonoBehaviour
{
    [SerializeField]
    protected Animator transition;

    public abstract void LoadPreTransition();
    public abstract void LoadPostTransition();
}
