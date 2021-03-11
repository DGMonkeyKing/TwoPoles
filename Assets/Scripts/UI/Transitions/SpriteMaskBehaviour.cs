using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteMaskBehaviour : MonoBehaviour 
{

    private Animator animator;
    private AsyncOperation ao = null;
    private bool safeLoad = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(ao != null)
        {
            if (ao.isDone)
            {
                animator.SetTrigger("SceneLoaded");
                ao = null;
            }
        }
    }

    public void LoadPostTransition()
    {
        //SafeLoad: LoadPostTransiction is called two times (one in normal and other in mirrered animation)
        if (!safeLoad)
        {
            SceneManager.UnloadSceneAsync("GameScene");
            SceneManager.sceneUnloaded += ReloadScene;
            safeLoad = true;
        }
        else
        {
            safeLoad = false;
        }
    }

    private void ReloadScene(Scene current)
    {
        ao = SceneManager.LoadSceneAsync(current.name, LoadSceneMode.Additive);
        SceneManager.sceneUnloaded -= ReloadScene;
    }
}
