using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// A class that represents the physical and visual changes of the 
/// </summary>
public class GrabModel : Model
{

    //stroes the data for the GrabModel and saves changes during play mode
    public GrabData data;

    ////range of grab determined by slider
    //[Tooltip("How far away he grabs")]
    //[Range(0f, 10f)]
    //public float grabRange;

    ////speed variable inverse to actual speed
    //[Tooltip("Smaller the number the faster it goes")]
    //[Range(0f, 0.7f)]
    //public float speed;

    ////distance from origin, the default is pretty good right now
    //[SerializeField]
    //protected Vector2 offSet = new Vector2(0.6f, 0.6f);

    //direction of ray and relative position of player
    public static Vector3 direction;

    Rigidbody myRB;

    Renderer myRender = new Renderer();

    //Game Object being used as grabbing hand/Arm
    [SerializeField]
    private RootAim rootAim;

    // Use this for initialization
    protected virtual void Awake ()
    {
        myRB = this.gameObject.GetComponent<Rigidbody>();
        myRender = this.GetComponent<Renderer>();
        rootAim = this.GetComponent<RootAim>();
        Vector3 trueOffSet = new Vector3(data.offSet.x, data.offSet.y, 0);
        transform.localPosition = trueOffSet;

    }

    protected virtual void Update()
    {
        if (!RootAim.facesRight)
            direction = Vector3.left;
        else
            direction = Vector3.right;
    }

    /// <summary>
    /// As long as button is pressed down it will propel hand towards maximum Reach
    /// </summary>

    public void grab()
    {   //appears whilegrabbing
        myRender.enabled = true;
        Vector3 position = transform.localPosition;
        Vector3 destination = new Vector3(data.grabRange, data.offSet.y, 0);
        Vector3 velocity = Vector3.zero;
        transform.localPosition = Vector3.SmoothDamp(position, destination, ref velocity, data.speed);
    }


    public virtual void release()
    {   //and disappears when you let go
        myRender.enabled = false;
        Vector3 trueOffSet = new Vector3(data.offSet.x, data.offSet.y, 0);
        transform.localPosition = trueOffSet;


    }

    private void OnCollisionEnter(Collision collision)
    {
        //if the object in question is "grabbed" then...
        if (collision.gameObject.tag == "Projectile")
        {

            /*Imagine a bunch of code that does a couple of things:
             * triggers the "destruction" of the target, 
             * the addition of an object
             * type Projectile to some kind to list
             * that retains the number of projectiles
             * up to a predetermined limit
             * Could also possibly code all that crap into enemies
             * seems easier
             */
        }
        else
            return;

    }

    private void OnCollisionExit(Collision collision)
    {

    }

}
