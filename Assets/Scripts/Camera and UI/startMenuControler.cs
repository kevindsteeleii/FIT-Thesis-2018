using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startMenuControler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public virtual void gameStart(){
		Debug.Log("Start");

	}

	public virtual void gameQuit(){
		Debug.Log("Quit");
	}

}
