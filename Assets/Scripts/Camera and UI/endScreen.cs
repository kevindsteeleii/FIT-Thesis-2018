using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endScreen : MonoBehaviour {
	
	public virtual void end(){
		Debug.Log("DA END");
		SceneManager.LoadScene ("20180428_Start Screen", LoadSceneMode.Single);
	}
}
