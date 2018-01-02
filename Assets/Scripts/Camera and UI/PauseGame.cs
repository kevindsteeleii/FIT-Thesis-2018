using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//Gimme some time to connect button inputs over here < Kev Note
public class PauseGame : Singleton<PauseGame>
{
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

    /// <summary>
    /// Commenting out on account of spooky language copy/paste helps no one
    /// </summary>
    //protected override void Awake()
    //{
        //ThesisDebuggingTools.ProxyController.onControllerKeyUp += (ThesisDebuggingTools.KeyPressed key) =>
        //{
        //    if (key == ThesisDebuggingTools.KeyPressed.start)
        //    {
        //        // create a method to toggle between pause & unpause
        //        Debug.Log("key press up: " + key.ToString());

        //        if (visible)
        //        {
        //Continue();
        //        }
        //        else
        //        {
        //            visible = true;
        //            //GameManager.instance.Pause();
        //        }
        //    }
        //};
        //DontDestroyOnLoad(this.GetComponentInParent<Canvas>());
    //}

    protected virtual void Start()
    {
        PlayerStats.instance.On_HPAmount_Sent += IsDead;
        Continue();
    }

    /// <summary>
    /// Method that changes the bool value of dead to based on the amount of HP left <Kev Note
    /// </summary>
    /// <param name="obj"></param>
    protected virtual void IsDead(int obj)
    {
        dead = (obj <= 0) ? true : false;
    }

    //DON'T FUCK WITH TIME!! < Kev Note
    //I WILL FUCK WITH TIME! ... I Will...
    protected virtual void Continue()
    {
        Debug.Log("RESUME");
        visible = false;
        gameOverScreen.SetActive(false);
        menu.SetActive(false);
    }

    /*Restart logic needs to reside in the Game Manager you only need to worry 
    about type void methods that uses a subscribed event as input or a trigger
    Mind you this is still a WIP gimme a minute... <Kev Note*/
    protected virtual void Restart()
    {
        //	//ok, we need a better way to reset the scene, because right now the reset button is going to reload the 
        //current scene, and as a standalone scene the scene that will be reset is the standalone UI
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        if (Restarted != null)
        {
            Restarted();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        /*checks the gameState and the button presses to make sure things work 
         * dependent of the current game state of the GameManager <Kev Note*/
        if (ButtonPressed() && GameManager.instance.GetState() == GameState.inGame && Paused != null)
        {
            Paused();
            visible = true;
        }

        //if the game is paused, and the pause button is pressed, hide the pause menu and unpause the game <coops's note
        else if (ButtonPressed() && GameManager.instance.gameState == GameState.pause && Unpaused != null)
        {
            Unpaused();
            visible = false;
        }

        menu.SetActive(visible);
        gameOverScreen.SetActive(dead);
    }
    //checks to see if the pause button is pressed and returns if the key pressed was true or false <coops's note
    protected virtual bool ButtonPressed()
    {
        bool pressed;
        return pressed = (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) ? true : false;
    }
}
