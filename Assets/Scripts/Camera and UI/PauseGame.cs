using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : Singleton<PauseGame> {
	public GameObject menu,Udied;
	public bool visible;
	public bool dead = false;

    /// <summary>
    /// Paused is an event that simply returns 
    /// </summary>
    public event Action Paused;
    public event Action Unpause;

    private void Awake()
    {
        ThesisDebuggingTools.ProxyController.onControllerKeyUp += (ThesisDebuggingTools.KeyPressed key) =>
        {
            if (key == ThesisDebuggingTools.KeyPressed.start)
            {
                // create a method to toggle between pause & unpause
                Debug.Log("key press up: " + key.ToString());

                if (visible)
                {
                    Continue();

                }
                else
                {
                    visible= true;
                    //GameManager.instance.Pause();
                }
            }
        };
    }

    private void Start()
    {
        if (Unpause != null)
        {
            Unpause();
        }
        if (Paused != null)
        {
            Paused();
        }

        PlayerStats.instance.HPAmount += IsDead;
    }

    /// <summary>
    /// Method that changes the bool value of dead to based on the amount of HP left
    /// </summary>
    /// <param name="obj"></param>
    private void IsDead (int obj)
    {
        dead = (obj <= 0) ? true : false;
    }

    //DON'T FUCK WITH TIME!!
    //I WILL FUCK WITH TIME! ... I Will...
    public void Continue(){
		Debug.Log("RESUME");
		visible=false;
		menu.SetActive(false);
        //GameManager.instance.StartGame();
	}

    /*Restart logic needs to reside in the Game Manager you only need to worry 
    about type void methods that use a subscribed event as input or a trigger*/

    //public void Restart(){
    //	//ok, we need a better way to reset the scene, because right now the reset button is going to reload the current scene, and as a standalone scene the scene that will be reset is the standalone UI
    //	/*
    //	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //       GameManager.instance.StartGame();*/
    //}

    // Update is called once per frame
    void Update () {
        /*checks the gameState and the button presses to make sure things work 
         * dependent of the current game state of the GameManager*/
  //      if (ButtonPressed() && GameManager.instance.GetState() == GameState.inGame)
  //      {
  //          visible= true;
  //          //GameManager.instance.Pause();
  //      }
		////if the game is paused, and the pause button is pressed, hide the pause menu and unpause the game <coops's note
  //      else if (ButtonPressed() && GameManager.instance.gameState == GameState.pause) 
  //      {
  //          visible= false;
  //          //GameManager.instance.StartGame();
  //      }

        //toggles visibility of pause menu based on button presses
        if (ButtonPressed())
        {
            visible= !visible;
        }

		//if (PlayerStats.instance.hp <= 0)
		//{
		//	dead = true;
		//	//GameManager.instance.GameOver ();
		//	//GameManager.instance.Pause();
		//}

        //Two events that transmit their events on pause and unpause
        

        menu.SetActive(visible);
		Udied.SetActive (dead);
	}
	//checks to see if the pause button is pressed and returns if the key pressed was ture or false <coops's note
    bool ButtonPressed()
    {
        bool pressed;
        return pressed = (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) ? true : false;
    }
}
