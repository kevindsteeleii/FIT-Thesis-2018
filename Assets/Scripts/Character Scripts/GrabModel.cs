using UnityEngine;

/// <summary>
/// A class that represents the physical and visual changes of the 
/// </summary>
public class GrabModel : Model
{

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
    protected virtual void Awake()
    {
        this.gameObject.tag = "Hand";
        myRender = this.GetComponent<Renderer>();
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
    {
        //appears whilegrabbing
        myRender.enabled = true;
        Vector3 position = transform.localPosition;
        Vector3 destination = new Vector3(data.grabRange, data.offSet.y, 0);
        Vector3 velocity = Vector3.zero;
        transform.localPosition = Vector3.SmoothDamp(position, destination, ref velocity, data.speed);
    }

    public void release()
    {
        //and disappears when you let go

        Vector3 trueOffSet = new Vector3(data.offSet.x, data.offSet.y, 0);
        transform.localPosition = trueOffSet;
        myRender.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<Enemy>().grabbable)
            {
                other.gameObject.GetComponent<Enemy>().becomeProjectile();
                Debug.Log("Grab");
            }


            else if (!other.gameObject.GetComponent<Enemy>().grabbable)
            {
                other.gameObject.GetComponent<Enemy>().takeDamage(damage);
                Debug.Log("Grab At");
            }
        }
    }



}
