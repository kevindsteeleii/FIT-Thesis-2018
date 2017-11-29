using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the class/script that deals with fall death in-game.
/// </summary>
public class DeathByFallBox : InstaDeath
{
    [SerializeField]
    Camera followTarget;
	// Use this for initialization
	protected override void Start () {
        base.Start();

        //insures that the followTarget is indeed the camera
		if (followTarget == null)
        {
           followTarget = FindObjectOfType<Camera>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        //this bit simply makes the deathBox follow along w/ the camera's x position
        Vector3 pos = this.transform.position;
        pos.x = followTarget.transform.position.x;
        transform.position = pos;
	}

    //as of this writing death box does not cause death or game over but does follow along with camera movement
}
