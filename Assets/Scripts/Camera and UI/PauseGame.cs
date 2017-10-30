using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : Singleton<PauseGame> {
	public GameObject menu;
	public bool visable;

    //DON'T FUCK WITH TIME!!
	//I WILL FUCK WITH TIME! ... I Will...
	public void unpause(){
		Debug.Log("Clicked");
		visable=false;
		menu.SetActive(false);
        GameManager.instance.StartGame();
	}
	public void Restart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.instance.StartGame();
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

        else if (ButtonPressed() && GameManager.instance.gameState == GameState.pause) 
        {
            visable = false;
            GameManager.instance.StartGame();
        }
        menu.SetActive(visable);
	}

    bool ButtonPressed()
    {
        bool pressed;
        return pressed = (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) ? true : false;
    }
}
