using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Houses logic, functions, etc. for the pause menu, game over, etc
/// </summary>
public class GUIManager : Singleton<GUIManager> {

    public GameObject menu, gameOverScreen;
    public bool visible;
    public bool dead = false;

    /// <summary>
    /// Paused event transmits when pause button is pressed
    /// </summary>
    public event Action Paused;

    /// <summary>
    /// Unpaused is transmitted when pause is exited and game is to resume
    /// </summary>
    public event Action Unpaused;

    /// <summary>
    /// Restarted is transmitted upon the restarting of the level/ game loop
    /// </summary>
    public event Action Restarted;

    protected override void Awake()
    {
        base.Awake();
    }

    public virtual void Continue()
    {
        Debug.Log("RESUME");
        visible = false;
        gameOverScreen.SetActive(false);
        menu.SetActive(false);
        Unpaused();
    }

    /*Restart logic needs to reside in the Game Manager you only need to worry 
    about type void methods that uses a subscribed event as input or a trigger
    Mind you this is still a WIP gimme a minute... <Kev Note*/
    public virtual void Restart()
    {
        if (Restarted != null)
        {
            Restarted();
            Continue();
        }
        //Loads the main/active scene asynchronously (the one with camera and player)
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        //Loads the scene with scene manager in additive mode right after but at time its called
        SceneManager.LoadScene(sceneBuildIndex: 2, mode: LoadSceneMode.Additive);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        /*checks the gameState and the button presses to make sure things work 
         * dependent of the current game state of the GameManager <Kev Note*/
        if (ButtonPressed() && GameManager.instance.GetState() == GameState.inGame && Paused != null)
        {
            Paused();
            visible = true;
        }

        menu.SetActive(visible);
        gameOverScreen.SetActive(dead);
    }

    //checks to see if the pause button is pressed and returns if the key pressed was true or false <coops's note
    public virtual bool ButtonPressed()
    {
        bool pressed;
        return pressed = (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) ? true : false;
    }
}
