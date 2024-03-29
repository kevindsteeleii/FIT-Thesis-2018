using UnityEngine;

public class playerControl : MonoBehaviour
{
    //variables for movement
    [Range(0f, 8f)]
    public float runSpeed;

    //static floats to be used for altering speed and movement horizontally
    public static float saveSpeed, saveMove;

    public static Rigidbody myRB;
    public static Animator myAnim;

    public static bool facingRight = true;

    Vector3 respawnPos;
    Quaternion rot;

    /// <summary>
    /// keeps position to be referred to outside
    /// </summary>
	public static Vector3 myPos;
    // Use this for initialization
    protected virtual void Awake()
    {
        runSpeed = 5.5f;
        saveSpeed = runSpeed;
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        //these particular settings brute force initial conditions as being airborne and not on the ground
        myAnim.SetBool("grounded", false);
        respawnPos = myRB.transform.position;
        rot = myRB.transform.rotation;
    }

    // Update is called once per frame gets expensive
    protected virtual void Update()
    {
        //establishes an updater for position of character to be used by other classes/scripts

        myPos = myRB.transform.position;
        //Debug print out to verify outputs/ data
        //		if (Input.anyKey) {
        //			Debug.Log (Input.inputString);
    }

    //depends upon physics objects
    void FixedUpdate()
    {

        //if false death-state is on all other actions cease
        if (!myAnim.GetBool("dead"))
        {
            float move = Input.GetAxis("Horizontal");
            myAnim.SetFloat("speed", Mathf.Abs(move));
            myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);

            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }

        }
        // respawns after fake death state upon space bar press ??
        //later to be replaced with a respawn screen w/ countdown and penalty at a percentage of in game currency/points

        else if (Input.GetKey(KeyCode.Space) && myAnim.GetBool("dead") == true)
        {
            transform.SetPositionAndRotation(respawnPos, rot);
            myAnim.SetBool("dead", false);
            myAnim.SetBool("grounded", true);
            facingRight = true;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

}