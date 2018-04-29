using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenuControler : MonoBehaviour {
	
	public virtual void gameStart(){
		Debug.Log("Start");
		SceneManager.LoadScene ("20180416_level_001_MLC", LoadSceneMode.Single);
		SceneManager.LoadScene ("20180418_level_001_Interactables_MLC", LoadSceneMode.Additive);
		SceneManager.LoadScene ("20180420_PlayerSetUp_Anims_KDS", LoadSceneMode.Additive);
		SceneManager.LoadScene ("20180426_level_001_Enemies", LoadSceneMode.Additive);
		SceneManager.LoadScene ("UI_StandAlone_20180413_MLCmod", LoadSceneMode.Additive);

	}

	public virtual void gameQuit(){
		Debug.Log("Quit");
	}

}
