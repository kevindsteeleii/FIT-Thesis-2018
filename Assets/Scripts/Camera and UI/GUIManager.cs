using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Houses logic, functions, etc. for the pause menu, game over, etc
/// </summary>
public class GUIManager : Singleton<GUIManager>
{
    public GameObject menu, gameOverScreen;
    bool visible;

    /// <summary>
    /// Event transmitted from GUI Manager when pause button is pressed
    /// </summary>
    public event Action On_PauseButton_Sent;

    /// <summary>
    /// Event transmitted from GUI Manager when pause is exited and game is set to resume
    /// </summary>
    public event Action On_ResumeButton_Sent;

    /// <summary>
    /// Event transmitted from GUI Manager upon the restarting of the level/ game loop
    /// </summary>
    public event Action On_RestartButton_Sent;

    protected virtual void Start()
    {
        //subscribes the event created by the 
        GameManager.instance.On_GameOverState_Sent += On_GameOver_Caught;
    }

    protected virtual void Continue()
    {
        Debug.Log("RESUME");
        visible = false;
        gameOverScreen.SetActive(false);

        On_ResumeButton_Sent();
    }

    //Subscriber that only is triggered by the initiation of the gameOver state change 
    public virtual void On_GameOver_Caught()
    {
        Debug.Log("GameOver GUI activated");
        gameOverScreen.SetActive(true);
    }

    /*Restart logic needs to reside in the Game Manager you only need to worry 
    about type void methods that uses a subscribed event as input or a trigger
    Mind you this is still a WIP gimme a minute... <Kev Note*/
    public virtual void Restart()
    {
        visible = false;
        gameOverScreen.SetActive(false);

        if (On_RestartButton_Sent != null)
        {
            Debug.Log("Sending Restart");
            On_RestartButton_Sent();
            Debug.Log("Restarted");
            //Continue();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        /*checks the gameState and the button presses to make sure things work 
         * dependent of the current game state of the GameManager <Kev Note*/

        if (ButtonPressed("Start") || ButtonPressed() && GameManager.instance.GetState() == GameState.inGame && On_PauseButton_Sent != null)
        {
            On_PauseButton_Sent();
            visible = true;
        }
        else if (ButtonPressed("Start") || ButtonPressed() && GameManager.instance.GetState() == GameState.pause && On_PauseButton_Sent != null)
        {
            Continue();
        }

        menu.SetActive(visible);
    }

    //checks to see if the pause button is pressed and returns if the key pressed was true or false <coops's note
    protected virtual bool ButtonPressed()
    {
        bool pressed;
        return pressed = (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) ? true : false;
    }

    protected virtual bool ButtonPressed(string button)
    {
        bool pressed;
        return pressed = (Input.GetButton(button)) ? true : false;
    }
}
