using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTransition : MonoBehaviour
{
    protected Animator transition;
    protected float transitionTime = 1f;


    protected abstract IEnumerator LoadPreTransition();
    protected abstract IEnumerator LoadPostTransition();
}
