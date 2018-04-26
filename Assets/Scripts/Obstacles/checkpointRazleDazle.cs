using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointRazleDazle : MonoBehaviour {
	public Collider respawnCheckpoint;
	public GameObject smokeEmiter;
	public Animator pillarAnim;

	// Use this for initialization
	void Start () {
		
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			pillarAnim.SetBool("Checkpoint", true);
			smokeEmiter.SetActive (true);
			Debug.Log ("Blip");
		}
	}
}
