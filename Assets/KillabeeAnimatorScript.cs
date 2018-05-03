using System;
using System.Collections.Generic;
using UnityEngine;

public class KillabeeAnimatorScript : MonoBehaviour {
    public event Action On_Killabee_Shot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void KillabeeFire()
    {
        Debug.Log("Killabee Firing");
        if (On_Killabee_Shot != null)
        {
            On_Killabee_Shot();
        }
    }
}
