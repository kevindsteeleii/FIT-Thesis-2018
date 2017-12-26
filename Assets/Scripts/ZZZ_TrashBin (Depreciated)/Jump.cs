using System;
using UnityEngine;

public class Jump : MonoBehaviour {
    public PlayerData data;
    PlayerHealth myHealth;

    //rigidboday and animator children of the player character 
    Animator myAnim;
    Rigidbody myRB;

    // Use this for initialization
    void Awake () {
        myAnim = this.GetComponent<Animator>();
        myAnim.SetBool("grounded", false);
        myHealth = this.gameObject.GetComponent<PlayerHealth>();
        myRB = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!myAnim.GetBool("dead"))
        {
            //checks if rigidbody is descending and increases rate of drop for snappier jump does not accelerate tthe same if slamming down
            if (myRB.velocity.y < 0)
            {
                myRB.velocity += Vector3.up * Physics.gravity.y * (data.fallMultiplier - 1) * Time.deltaTime;
            }

            else if (myRB.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                myRB.velocity += Vector3.up * Physics.gravity.y * (data.lowJumpMultiplier - 1) * Time.deltaTime;
            }

            bool jumper = jumpCheck("Jump", "grounded", true, myAnim);
            if (Input.GetButton("Jump")) // { Debug.Log("Jumping"); }
            if (jumper)
            {
                Jumper();
            }
            if (Mathf.Abs(myRB.velocity.y) == 0)
            {
                myAnim.SetBool("grounded", true);
                myAnim.SetBool("airborne", false);
            }
        }
    }

    bool jumpCheck(String button, String state, bool flag, Animator myAnim)
    {
        bool acted;
        acted = (Input.GetButton(button) && myAnim.GetBool(state) == flag) ? true : false;
        return acted;
    }

    public virtual void Jumper()
    {
        myAnim.SetBool("grounded", false);
        myAnim.SetBool("airborne", true);
        myRB.velocity = new Vector3(myRB.velocity.x, data.jumpHeight, 0);
    }
}
