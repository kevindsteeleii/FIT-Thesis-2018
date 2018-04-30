using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenBetterJumpFinal : MonoBehaviour
{
    //initiates the mario jump where after the peak of jump character's gravity snappily increases
    //reduces float
    public PlayerData playData;
 
    float fallMultiplier = 2.5f;

    //establishes the lowest jump possible upon quick release of jump button 
    float lowJumpMultiplier = 5f;
    // Use this for initialization
    Rigidbody myRB;
    Animator myAnim;

    //for jumping player starts suspended above ground by default is not on ground
    float jumpHeight = 5f;

    protected virtual void Awake()
    {
        fallMultiplier = playData.fallMultiplier;
        lowJumpMultiplier = playData.lowJumpMultiplier;
        jumpHeight = playData.jumpHeight;

        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if rigidbody is descending and increases rate of drop for snappier jump does not accelerate tthe same if slamming down
        if (myRB.velocity.y < 0)
        {
            //myAnim.SetBool("Jump", false);
            //myAnim.SetBool("Falling", true);
            myRB.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        else if (myRB.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            myRB.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
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

        float vertSpeed = 0;
        vertSpeed = (myAnim.GetBool("grounded")) ?0:myRB.velocity.y;
        myAnim.SetFloat("vertSpeed", vertSpeed);
    }

    protected virtual void Jump()
    {
        myAnim.SetBool("grounded", false);
        //myAnim.SetBool("airborne", true);
        //myAnim.SetFloat("vertSpeed", 0);
        myRB.velocity = new Vector3(myRB.velocity.x, jumpHeight, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            myAnim.SetBool("grounded", true);
            myAnim.SetBool("slam", false);
            //myAnim.SetFloat("vertSpeed", 0);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            myAnim.SetBool("grounded", false);
            //myAnim.SetFloat("vertSpeed", myRB.velocity.y);
        }
    }
}
