using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {
	public GameObject menu;
	public bool visable;

	// Use this for initialization
	void Start () {

	}
	public void unpause(){
		Debug.Log("Clicked");
		visable=false;
		menu.SetActive(false);
		Time.timeScale = 1;
	}
	public void restart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = 1;
	}
		
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.Space)){
			if (visable== true){
				visable = false;
				Time.timeScale = 1;
			}
			else if (visable == false){
				visable = true;
				Time.timeScale = 0;
			}
            menu.SetActive(visable);
		}			
	}
}
