using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfazPause : MonoBehaviour 
{
    [SerializeField]
    private PauseMenu m_PauseMenu;

    private static bool isGamePaused = false;
    public static bool IsGamePaused
    {
        get {return isGamePaused;}
    }

    void Start()
    {
        m_PauseMenu.gameObject.SetActive(false);
    }

    void Update()
    {
        //Check Start button is pressed (whether or not is also paused)
        var pauseInput = Input.GetButtonDown ("Pause");
#if LOG
        Debug.Log("pauseInput: " + pauseInput);
#endif
        if(pauseInput)
        {
            if(isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame ()
    {
        
        Debug.Log("PAUSA");
        Time.timeScale = 0f;
        AudioListener.pause = true;
        
        isGamePaused = true;

        m_PauseMenu.gameObject.SetActive(true);
    }

    void ResumeGame ()
    {
        
        Debug.Log("RESUME");
        Time.timeScale = 1f;
        AudioListener.pause = false;
        
        isGamePaused = false;
        
        m_PauseMenu.gameObject.SetActive(false);
    }
}
