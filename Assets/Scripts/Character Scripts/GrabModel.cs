using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabModel : MonoBehaviour {

    //range of grab uses slider before cemented into code
    [Range(0f, 3f)]
    public float grabRange;


    //direction of ray and relative position of player
    private Vector3 direction;
    private Vector3 myPos;

    //Game Object being used as grabbing hand/Arm
    [SerializeField]
    private GameObject hand;

    // Use this for initialization
    protected virtual void Awake()
    {
        if (!hand )        {
            hand = this.gameObject;
        }
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        myPos = playerControl.myPos;

        //reverses direction of Raycast to either left or right
        if (!playerControl.facingRight)
            direction = Vector3.left;
        else
            direction = Vector3.right;

        // on grab button attempts to grab
        if (Input.GetButtonDown("Grab"))
        {
            grab();
        }

    }

    protected virtual void grab()
    {
        ////look at Physics.OverlapSphere for detection and tags
        //Debug.Log("The direction is " + direction);
        ////		RaycastHit hit;
        //Vector3 forward = transform.TransformDirection(direction) * 10;
        ////		Debug.DrawRay(myPos,)

        //if (Physics.Raycast(myPos, direction, grabRange))
        //{
        //    Debug.Log("Something was HIT!!");

        //}

    }

}
