using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ArmAdds : GrabModel {

    Collider collider;
	// Use this for initialization
	void Start () {
        collider = this.gameObject.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
