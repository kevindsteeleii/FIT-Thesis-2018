using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Houses logic, functions, etc. for the pause menu, game over, etc
/// </summary>
public class GUIManager : Singleton<GUIManager>
{

    public GameObject menu, gameOverScreen;
    public bool visible { get; private set; }
    public bool isDead { get; private set; }

    /// <summary>
    /// Paused event transmits from GUI Manager when pause button is pressed
    /// </summary>
    public event Action Paused;

    /// <summary>
    /// Unpaused is transmitted from GUI Manager when pause is exited and game is to resume
    /// </summary>
    public event Action Unpaused;

    /// <summary>
    /// Restarted is transmitted from GUI Manager upon the restarting of the level/ game loop
    /// </summary>
    public event Action Restarted;

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start ()
    {
        isDead = false;
        //subscribes the event created by the 
        GameManager.instance.GameOverStateEvent += OnGameOver;
    }

    public virtual void Continue()
    {
        Debug.Log("RESUME");
        visible = false;
        gameOverScreen.SetActive(false);
        //menu.SetActive(false);
        Unpaused();
    }

    //Subscriber that only is triggered by the initiation of the gameOver state change 
    public virtual void OnGameOver()
    {
        Debug.Log("GameOver GUI activated");
        gameOverScreen.SetActive(true);

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
    protected virtual void Update()
    {
        /*checks the gameState and the button presses to make sure things work 
         * dependent of the current game state of the GameManager <Kev Note*/
        if (ButtonPressed("Start") || ButtonPressed() && GameManager.instance.GetState() == GameState.inGame && Paused != null)
        {
            Paused();
            visible = true;
        }
        else if (ButtonPressed("Start") || ButtonPressed() && GameManager.instance.GetState() == GameState.pause && Paused != null)
        {
            Continue();
        }

        if (ButtonPressed("Start"))
        {
            Debug.Log("Pressed Start Button");
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
