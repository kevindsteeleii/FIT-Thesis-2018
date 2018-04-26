using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformStick : MonoBehaviour {
	public GameObject platform;


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			other.transform.parent =  platform.transform;
			Debug.Log ("plonk");
		}
	}
		
	private void OnTriggerExit (Collider other){
		other.transform.parent = null;
	}
}
