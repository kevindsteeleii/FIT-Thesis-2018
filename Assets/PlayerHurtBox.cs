using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBox : MonoBehaviour {
    /// <summary>
    /// Event sent from hurtbox to the listener in the playerstats class to tally damage taken
    /// </summary>
    public event Action<int> On_Damage_Received;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //an attempt to clean up how enemies interact with player character in an attack context
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBox" && other.gameObject.layer == 12)
        {
            //Debug.Log("Enemy struck player!!"); //it works
        }
    }
}
