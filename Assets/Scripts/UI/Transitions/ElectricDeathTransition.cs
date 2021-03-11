using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElectricDeathTransition : AbstractTransition
{
    void Awake()
    {
        gameObject.SetActive(false);
    }

    public override void LoadPreTransition()
    {
        gameObject.SetActive(true);
        transition.SetTrigger("ElectricDeath");
    }
    public override void LoadPostTransition()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
