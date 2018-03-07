using System;
using UnityEngine;
/// <summary>
/// Takes input for the shooting/throwing of projectiles
/// </summary>
public class LaunchController : Controller {
    LaunchModel launcherBody;

	// Use this for initialization
	protected virtual void Start () {
		if (launcherBody == null)
        {
            launcherBody = this.GetComponent<LaunchModel>();
        }
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		
	}
}
