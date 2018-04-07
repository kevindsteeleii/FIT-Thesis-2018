using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOverGUI : Singleton<GameOverGUI> {

    public GameObject gameOverScreen;

    /// <summary>
    /// Event transmitted from GUI Manager upon the restarting of the level/ game loop
    /// </summary>
    public event Action On_Restart_Sent;

    public event Action On_QuitOut_Sent;

    // Use this for initialization
    void Start () {
        //subscribes the event created by the 
        GameManager.instance.On_GameOverState_Sent += OnGameOverState;
        On_Restart_Sent += GameManager.instance.OnRestartButton;
    }

    public virtual void Quit()
    {
        gameOverScreen.SetActive(false);

        if (On_QuitOut_Sent != null)
        {
            Debug.Log("Quiting...");
            On_QuitOut_Sent();
        }
    }

    public virtual void Restart()
    {
        gameOverScreen.SetActive(false);

        if (On_Restart_Sent != null)
        {
            Debug.Log("Sending Restart GO");
            On_Restart_Sent();
            Debug.Log("Restarted GO");
        }
    }

    //Subscriber that only is triggered by the initiation of the gameOver state change 
    public virtual void OnGameOverState()
    {
        gameOverScreen.SetActive(true);
    }

}
