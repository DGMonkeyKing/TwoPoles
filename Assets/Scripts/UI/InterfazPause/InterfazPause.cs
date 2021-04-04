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
        var pauseInputP1 = Input.GetButtonDown ("Pause - P1");
        var pauseInputP2 = Input.GetButtonDown ("Pause - P2");

        var pauseInput = pauseInputP1 || pauseInputP2;
        m_PauseMenu.SetPlayerPaused(pauseInputP1, pauseInputP2);
#if LOG
        Debug.Log("pauseInput: " + pauseInput);
#endif
        if(pauseInput)
        {
            if(isGamePaused)
            {
                m_PauseMenu.ResumeGame();
                isGamePaused = false;
                m_PauseMenu.gameObject.SetActive(false);
            }
            else
            {
                m_PauseMenu.PauseGame();
                isGamePaused = true;
                m_PauseMenu.gameObject.SetActive(true);
            }
        }
    }
}
