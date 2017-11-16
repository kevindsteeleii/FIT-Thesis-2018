using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// enum used to establish the behavior state of the platform the 
/// default is normal and upon 
/// </summary>
public enum PlatformBehavior { Normal, Disappearing, Moving, Falling };


/// <summary>
/// Enumeration that handles the kind of movement the platform performs if its behavior enumeration = moving and defaults to none if any other type of behavior
/// </summary>
public enum MovementType { None = 0, Vertical, Horizontal, Diagonal};

/// <summary>
/// Class used to create platforms and manage their behavior
/// </summary>
public class Platforms : MonoBehaviour

{
    //collider to be used with the the platformbehavior enum
    Collider collider;
    GameObject platform;

    // points for the enemy patrol "paths" on singular platforms when generated randomly center point is the center of the object itself used for calculations and the like
    //public Vector3 startPatrol, endPatrol;

    //center point is the center of the object itself used for calculations and the like
    Vector3 centerPoint;

    [SerializeField]
    Transform endPoint;

    /// <summary>
    /// The absolute value distance from the center to either edge of the platform
    /// </summary>
    public float delta;

    /// <summary>
    /// If true makes the Platform 
    /// </summary>
    public bool isRandomMovement;
    

    /// <summary>
    /// Enumeration that assigns default behavior of platform
    /// </summary>
    public PlatformBehavior behavior = PlatformBehavior.Normal;
    public MovementType platformMovement = MovementType.None;

    // Use this for initialization
    private void Start()
    {
        if (collider == null)
        {
            collider = this.GetComponent<Collider>();
        }

        platform = this.gameObject;

        delta = collider.bounds.extents.x;

        // just for debugging
        // remove later
        PlatformSpawner.instance.SpawnNewPlatformAt(endPoint.position);

        if (isRandomMovement)
        {
            RandBehavior();
        }
    }

    //switches the type of behavior on the fly if necessary later
    void ChangeBehavior(PlatformBehavior newBehavior)
    {
        switch (newBehavior)
        {
            case PlatformBehavior.Disappearing:
                //choose an obstacle/platform behavior
                break;
            case PlatformBehavior.Normal:
                //does nothing, supports weight and platforming
                break;

            case PlatformBehavior.Moving:
                //choose an obstacle/platform behavior
                break;
            case PlatformBehavior.Falling:
                //choose an obstacle/platform behavior
                break;
            default:
                break;
        }
        behavior = newBehavior;
        Debug.Log("WARNING!! This is a " + behavior + " type of Platform.");
    }

    public PlatformBehavior GetBehavior()
    {
        return this.behavior;
    }

    public void RandBehavior ()
    {
        int myBehavior = UnityEngine.Random.Range(0, 3);
        switch (myBehavior)
        {
            case 0:
                platformMovement = MovementType.None;
                break;
            case 1:
                platformMovement = MovementType.Vertical;
                break;
            case 2:
                platformMovement = MovementType.Horizontal;
                break;
            case 3:
                platformMovement = MovementType.Diagonal;
                break;

            default :
                break;
        }
    }

    /// <summary>
    /// Returns the center point of platform
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCenter()
    {
        return centerPoint;
    }

    //deals with the positional data of the particular platform obstacle
    void FixedUpdate()
    {
        //assigns the centerPoint variable the value of the center of the collider of the platform
        centerPoint = collider.bounds.center;
        //distance from center to end of the object on x-axis
        float width = collider.bounds.extents.x;

        //this will have to be changed for non-boxy platforms/obstacles in the future
        //startPatrol = new Vector3(centerPoint.x - width, centerPoint.y, centerPoint.z);
        //endPatrol = new Vector3(centerPoint.x + width, centerPoint.y, centerPoint.z);
    }
}
