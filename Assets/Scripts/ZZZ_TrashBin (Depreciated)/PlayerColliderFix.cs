using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderFix : MonoBehaviour {

    CapsuleCollider myCol; 
	// Use this for initialization
	void Start () {
        myCol = gameObject.GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        myCol.center = gameObject.transform.position;
	}
}
