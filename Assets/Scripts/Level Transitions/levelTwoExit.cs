using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelTwoExit : MonoBehaviour {

	private void OnTriggerEnter(){
		SceneManager.LoadScene ("End Credit", LoadSceneMode.Single);
		Debug.Log("zip");

	}
}
