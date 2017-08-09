using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to follow player character w/o problematic rotations
/// or other adjustments to models etc.
/// Works as inteded do not directly adjust or change
/// </summary>
public class RootAim : MonoBehaviour
{
    /// <summary>
    /// Used as a basis for both the relativeOffset for grab and starting
    /// Vector3 to figure out the direction of the aimed throw.
    /// </summary>
    public event Action<Vector3> aimPosition;

    //player as target
    [SerializeField]
    private Transform target;

    public static bool facesRight;

    public static Vector3 reticle;

   
    public virtual void Awake()    {
        facesRight = true;
        reticle = GameObject.FindGameObjectWithTag("AimReticule").transform.position;
    }


    // Update is called once per frame
    public virtual void FixedUpdate()
    {

        Vector3 desiredPosition = target.position;
        desiredPosition.z = 0f;
        transform.position = desiredPosition;

        float move = Input.GetAxis("Horizontal");
        if (move > 0 && !facesRight)
        {
            flipMe();
        }
        else if (move < 0 && facesRight)
        {
            flipMe();
        }

    }

    public virtual void flipMe()
    {
        facesRight = !facesRight;
        //Debug.Log("flipping");
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }
    
}

