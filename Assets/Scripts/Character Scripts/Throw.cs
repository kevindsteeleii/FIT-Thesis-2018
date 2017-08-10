using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles the throwing and aimed throw mechanics
/// </summary>
public class Throw : MonoBehaviour {

    [SerializeField]
    private Ammo ammo;

    Rigidbody myRb;

    // Use this for initialization
    protected virtual void Awake () {
        myRb = this.GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    protected virtual void Update () {
        //Mind you the "Grab" button doubles as 
        if (Input.GetButton("Grab")&&!Ammo.emptyClip)   {
            Ammo.shootLoad();
        }
    }

    protected void throwAngle()
    {

    }
}
