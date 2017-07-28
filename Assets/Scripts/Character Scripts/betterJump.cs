using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterJump : MonoBehaviour {
	//initiates the mario jump where after the peak of jump character's gravity snappily increases
	//reduces float

	[Range (1,20)]
	public float fallMultiplier = 2.5f;

	//establishes the lowest jump possible upon quick release of jump button 
	[Range (1,10)]
	public float lowJumpMultiplier = 5f;
	// Use this for initialization
	Rigidbody myRB;
    Animator myAnim;


    //for jumping player starts suspended above ground by default is not on ground
    [Range(0, 40)]
    public float jumpHeight;

    protected virtual void Awake () {
        jumpHeight = 7f;
		myRB = GetComponent<Rigidbody> ();
        myAnim = GetComponent<Animator>();
    }

//	void Start () {
//		
//	}

	// Update is called once per frame
	void Update () {
		 //checks if rigidbody is descending and increases rate of drop for snappier jump does not accelerate tthe same if slamming down
		if (myRB.velocity.y < 0 ) {
			myRB.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier -1) * Time.deltaTime;
		} 

		else if (myRB.velocity.y > 0 && !Input.GetButton("Jump")) {
			myRB.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier -1) * Time.deltaTime;
		} 

	}

    protected virtual void FixedUpdate()
    {
        //if false death-state is on all other actions cease
        if (!myAnim.GetBool("dead"))
        {
            //this works but the in air or airborne cycle does not work it strobes
            if (Input.GetButton("Jump") && myAnim.GetBool("grounded") == true)
            {
                Jump();
            }
        }
    }


    protected virtual void Jump()
    {
        myAnim.SetBool("grounded", false);
        myRB.velocity = new Vector3(myRB.velocity.x, jumpHeight, 0);
    }
    /// <summary>
    /// Stops the movement of character to allow for animation hitStopping
    /// </summary>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            myAnim.SetBool("grounded", true);
    }

    void OnCollisionExit(Collision collision)
    {
        if (Mathf.Abs(myRB.velocity.y) > 0)
            myAnim.SetBool("grounded", false);
    }

}
