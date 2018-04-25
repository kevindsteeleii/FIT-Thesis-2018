using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformStick : MonoBehaviour {
	public GameObject platform;
	public GameObject Player;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			Player.transform.parent = platform.transform;
			Debug.Log ("plonk");
		}
	}
}
