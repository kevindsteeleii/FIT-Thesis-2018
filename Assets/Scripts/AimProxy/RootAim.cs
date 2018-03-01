﻿using UnityEngine;

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

    //player as target
    [SerializeField]
    Transform target;

    public static bool facesRight;
    public static Vector3 aimPos;
    public static int direction = 1;

    [Range (0f,3f)]
    public float vertAdjustment;

    //Initiates the default state
    public virtual void Awake()
    {
        facesRight = true;
    }
    
    // Update is called once per frame
    public virtual void FixedUpdate()    {
        aimPos = this.transform.position;
        Vector3 desiredPosition = target.position;
        desiredPosition.y = vertAdjustment+target.position.y;
        desiredPosition.z = 0f;
        transform.position = desiredPosition;
        float move = Input.GetAxis("Horizontal");
        //float mover = Input.GetAxis("JoystickHorizontal");

        if (move > 0 && !facesRight || move < 0 && facesRight )
        {
            FlipMe();
        }
    }

    public virtual void FlipMe()
    {
        facesRight = !facesRight;
        direction *= -1;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

    public virtual GameObject GetAim()
    {
        return gameObject;
    }
}

