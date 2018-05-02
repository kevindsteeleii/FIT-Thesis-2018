using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelOneExit : MonoBehaviour {

	// Use this for initialization
	private void OnTriggerEnter(Collider other){
		if (other.tag == "Player"){
			SceneManager.LoadScene ("20180502_level_002_MLC", LoadSceneMode.Single);
			SceneManager.LoadScene ("20180502_level_002_Interactables_MLC", LoadSceneMode.Additive);
			SceneManager.LoadScene ("20180420_PlayerSetUp_Anims_KDS", LoadSceneMode.Additive);
			SceneManager.LoadScene ("20180502_level_002_Enemies", LoadSceneMode.Additive);
			SceneManager.LoadScene ("UI_StandAlone_20180413_MLCmod", LoadSceneMode.Additive);
		}
	}
}
