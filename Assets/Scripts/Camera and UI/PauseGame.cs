using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : Singleton<PauseGame> {
	public GameObject menu,Udied;
	public bool visable;
	public bool ded = false;

	private void Awake () {
		ThesisDebuggingTools.ProxyController.onControllerKeyUp += (ThesisDebuggingTools.KeyPressed key) => {
			if (key == ThesisDebuggingTools.KeyPressed.start) {
				// create a method to toggle between pause & unpause
				Debug.Log("key press up: " + key.ToString());

				if (visable) {
					unpause();
				} else {
					visable = true;
					GameManager.instance.Pause();
				}
			}
		};
	}

    //DON'T FUCK WITH TIME!!
	//I WILL FUCK WITH TIME! ... I Will...
	public void unpause(){
		Debug.Log("RESUME");
		visable=false;
		menu.SetActive(false);
        GameManager.instance.StartGame();
	}


	public void Restart(){
		//ok, we need a better way to reset the scene, because right now the reset button is going to reload the current scene, and as a standalone scene the scene that will be reset is the standalone UI
		/*
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.instance.StartGame();*/
	}
		
	// Update is called once per frame
	void Update () {
        /*checks the gameState and the button presses to make sure things work 
         * dependent of the current game state of the GameManager*/
        if (ButtonPressed() && GameManager.instance.GetState() == GameState.inGame)
        {
			
            visable = true;
            GameManager.instance.Pause();
        }
		//if the game is paused, and the pause button is pressed, hide the pause menu and unpause the game <coops's note
        else if (ButtonPressed() && GameManager.instance.gameState == GameState.pause) 
        {
            visable = false;
            GameManager.instance.StartGame();
        }

		if (PlayerStats.instance.hp <= 0)
		{
			ded = true;
			GameManager.instance.GameOver ();
			GameManager.instance.Pause();
		}
        menu.SetActive(visable);
		Udied.SetActive (ded);
	}
	//checks to see if the pause button is pressed and returns if the key pressed was ture or false <coops's note
    bool ButtonPressed()
    {
        bool pressed;
        return pressed = (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) ? true : false;
    }
}
