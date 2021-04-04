using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour 
{
    [SerializeField]
    private GameObject[] menuPauseItems;

    private int cursorPosition = 0;
    private float verticalInput;
    private string playerPaused = "P1";
    private bool stickDownLast = false;

    void Update()
    {
        //Aquí tenemos que mover el cursor dependiendo de donde estemos con un Lerp
        //Check if up or down is pressed. Aritmética modular

        verticalInput = Input.GetAxisRaw ("Vertical - " + playerPaused);

        //If horizontalInput is negative, dale la vuelta.
        if (verticalInput > 0) 
        {
            if(!stickDownLast) cursorPosition++;
            stickDownLast = true;
        }
        else if (verticalInput < 0)
        {
            if(!stickDownLast) cursorPosition--;
            stickDownLast = true;
        }
        else
            stickDownLast = false;
    
        cursorPosition = UtilMath.nfmod(cursorPosition, menuPauseItems.Length);

        //Now, we have the position, lets check the Y-Axis of tyhe element menuPauseItems[cursorPosition]
        GameObject item = menuPauseItems[cursorPosition];
    }
    public void Options()
    {

    }

    public void PauseGame ()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void ResumeGame ()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void SetPlayerPaused(bool pauseInputP1, bool pauseInputP2)
    {
        if(pauseInputP1) playerPaused = "P1";
        if(pauseInputP2) playerPaused = "P2";
    }
}
