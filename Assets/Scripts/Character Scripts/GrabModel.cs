using UnityEngine;

/// <summary>
/// A class that represents the physical and visual changes of the 
/// </summary>
public class GrabModel : Model  {

    //stroes the data for the GrabModel and saves changes during play mode
    public GrabData data;

    //direction of ray and relative position of player
    public static Vector3 direction;

    //renderer used to make the "hand" disappear 
    Renderer myRender = new Renderer();

    //determines damage done by failed grab
    [Tooltip("Determines damage hand does to enemy on failed grab attempt")]
    [Range(0, 15)]
    public int damage;

    // Use this for initialization
    protected virtual void Awake ()    {
        this.gameObject.tag = "Hand";
        myRender = this.GetComponent<Renderer>();
        Vector3 trueOffSet = new Vector3(data.offSet.x, data.offSet.y, 0);
        transform.localPosition = trueOffSet;
    }

    protected virtual void Update()    {
        if (!RootAim.facesRight)
            direction = Vector3.left;
        else
            direction = Vector3.right;
    }

    /// <summary>
    /// As long as button is pressed down it will propel hand towards maximum Reach
    /// </summary>

    public void grab()    {  
        //appears whilegrabbing
        myRender.enabled = true;
        Vector3 position = transform.localPosition;
        Vector3 destination = new Vector3(data.grabRange, data.offSet.y, 0);
        Vector3 velocity = Vector3.zero;
        transform.localPosition = Vector3.SmoothDamp(position, destination, ref velocity, data.speed);
    }

    public void release()    {  
        //and disappears when you let go
        
        Vector3 trueOffSet = new Vector3(data.offSet.x, data.offSet.y, 0);
        transform.localPosition = trueOffSet;
        myRender.enabled = false;
    }

    public void OnCollisionEnter(Collision collision)    {
        //if the object in question is "grabbed" then...
        //if (collision.gameObject.CompareTag("Projectile"))  {
            //adds a single object type Projectile to a list of projectiles
           // Ammo.load();
            //Debug.Log(collision.gameObject.tag);
            /*Imagine a bunch of code that does a couple of things:
             * triggers the "destruction" of the target, the addition of an object
             * type Projectile to some kind to list that retains the number of projectiles
             * up to a predetermined limit. Could also possibly code all that crap into enemies
              seems easier */
       // }
       // else
          //  return;
    }    
}
