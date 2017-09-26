using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class PauseGame : MonoBehaviour {
	public GameObject menu;
	public bool visable;

	// Use this for initialization
	void Start () {

	}
	public void Onclick(){
		Debug.Log("Clicked");
		visable=false;
		menu.SetActive(false);
	}
		
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.Space)){
			if (visable== true){
				visable = false;
			}
			else if (visable == false){
				visable = true;
			}
            menu.SetActive(visable);
		}			
	}
}
