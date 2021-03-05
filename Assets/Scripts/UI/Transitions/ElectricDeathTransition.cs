using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElectricDeathTransition : AbstractTransition
{
    protected override IEnumerator LoadPreTransition()
    {
        transition.SetTrigger("ElectricDeath");
        yield return null;
    }
    protected override IEnumerator LoadPostTransition()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return null;
    }
}
