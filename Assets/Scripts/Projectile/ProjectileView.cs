using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles transform and positioning data
/// </summary>
public class ProjectileView : View {

    GameObject player;
	// Use this for initialization
	protected virtual void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
