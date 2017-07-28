using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles transform and positioning data
/// </summary>
public class ProjectileView : View {

    //used as reference point in place of 
    GameObject player;
	// Use this for initialization
	protected virtual void Awake () {
        player = GameObject.FindGameObjectWithTag("RootAim");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
