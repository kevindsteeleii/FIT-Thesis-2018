using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class used to 
/// </summary>
public class DeathByFallBox : InstaDeath
{
    [SerializeField]
    Camera followTarget;
	// Use this for initialization
	protected override void Start () {
		if (followTarget == null)
        {
           followTarget = FindObjectOfType<Camera>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = this.transform.position;
        pos.x = followTarget.transform.position.x;
        transform.position = pos;
	}

    //as of this writing death box does not cause death or game over but does follow along with camera movement
}
