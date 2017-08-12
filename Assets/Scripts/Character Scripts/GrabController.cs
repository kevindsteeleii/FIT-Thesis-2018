using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class to take in input that effects hand model 
/// </summary>
public class GrabController : Controller {

    //The GrabModel, and it's collider, may add or subtract based on what the final rig entails 
    [SerializeField]
    GrabModel handModel;

	// Use this for initialization
	void Awake () {
        // check to see if the model variable is empty
        if (!handModel)        {
            handModel = this.gameObject.GetComponent<GrabModel>();
        }
    }
	
	// Physics Changes
	void FixedUpdate ()
    {
		if (Input.GetButton("Grab")&&!Ammo.fullCap) {

            handModel.grab();
            Ammo.load();
        }

        if (!Input.GetButton("Grab") && Ammo.fullCap) {
            handModel.release();
        }
    }

}
