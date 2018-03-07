using UnityEngine;

/// <summary>
/// A class that represents the physical and visual changes of the 
/// </summary>
public class GrabModel : Model
{

    //stores the data for the GrabModel and saves changes during play mode
    public GrabData data;

    //renderer used to make the "hand" disappear 
    Renderer myRender = new Renderer();

    //determines damage done by failed grab
    [Tooltip("Determines damage hand does to enemy on failed grab attempt")]
    [Range(0, 15)]
    public int damage;

    // Use this for initialization
    protected virtual void Awake()
    {
        this.gameObject.tag = "Hand";
        myRender = this.GetComponent<Renderer>();
        Vector3 trueOffSet = new Vector3(data.offSet.x, data.offSet.y, 0);
        transform.localPosition = trueOffSet;
        this.GetComponent<SphereCollider>().enabled = false;
    }

    /// <summary>
    /// As long as button is pressed down it will propel hand towards maximum Reach.
    /// Additionally, it sets the collider to active on grab and activates the renderer to make it visible as well
    /// </summary>

    public void grab()
    {
        //appears whilegrabbing
        this.GetComponent<SphereCollider>().enabled = true;
        myRender.enabled = true;
        Vector3 position = transform.localPosition;
        Vector3 destination = new Vector3(data.grabRange, data.offSet.y, 0);
        Vector3 velocity = Vector3.zero;
        transform.localPosition = Vector3.SmoothDamp(position, destination, ref velocity, data.speed);
    }

    /// <summary>
    /// Upon release of the grab button the collider deactivates as well as the renderer
    /// making it invisible as well
    /// </summary>
    public void release()
    {
        //and disappears when you let go
        Vector3 trueOffSet = new Vector3(data.offSet.x, data.offSet.y, 0);
        transform.localPosition = trueOffSet;
        myRender.enabled = false;
        this.GetComponent<SphereCollider>().enabled = false;
    }
}
