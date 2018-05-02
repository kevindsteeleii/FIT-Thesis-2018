using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelTwoExit : MonoBehaviour {

	private void OnTriggerEnter(){
		SceneManager.LoadScene ("20180425_Start Screen", LoadSceneMode.Single);
		Debug.Log("zip");

	}
}
