using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// New and Improved consolidated Player Controller that handles movements, jumps, attacks and whatnot.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public PlayerData data;
    PlayerHealth myHealth;

    //rigidboday and animator children of the player character 
    Animator myAnim;
    Rigidbody myRB;
    public static bool facingRight;

    //respawn position
    Vector3 respawnPos;
    //respawn rotation
    Quaternion rot;

    /// <summary>
    /// keeps position to be referred to outside
    /// </summary>
    public static Vector3 myPos;
    PlayerHealth health;
    /// <summary>
    /// Delegates used to speed up runtime of checking the parameters and buttons of a resulting
    /// Animation/Action
    /// </summary>
    public delegate bool ActionTaken(String button, String state, bool flag, Animator myAnim);

    /// <summary>
    /// Combo Action taken is simply a a two-input version of Action Taken
    /// </summary>
    public delegate bool ComboActionTaken(String button1, String button2, String state, bool flag, Animator myAnim);

    //have the delegates equal two specific generic functions that produce bools based on input and animator parameters
    ActionTaken takeAction = ActionTook;
    ComboActionTaken comboAction = ComboActionTake;

    public void Awake()
    {
        health = this.GetComponent<PlayerHealth>();
        myAnim = this.GetComponent<Animator>();
        myRB = this.GetComponent<Rigidbody>();
        facingRight = true;
        myAnim.SetBool("grounded", false);
        respawnPos = myRB.transform.position;
        rot = myRB.transform.rotation;
        myHealth = this.gameObject.GetComponent<PlayerHealth>();



    }

    public void Update()
    {
        myPos = myRB.transform.position;

        //checks if rigidbody is descending and increases rate of drop for snappier jump does not accelerate tthe same if slamming down
        if (myRB.velocity.y < 0)
        {
            myRB.velocity += Vector3.up * Physics.gravity.y * (data.fallMultiplier - 1) * Time.deltaTime;
        }

        else if (myRB.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            myRB.velocity += Vector3.up * Physics.gravity.y * (data.lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }

    // Update is called once per physics action
    void FixedUpdate()
    {

        bool undead = takeAction("Respawn", "dead", true, myAnim);
        //if false death-state is on all other actions cease
        if (!myAnim.GetBool("dead") )
        {


            float move = Input.GetAxis("Horizontal");
            myAnim.SetFloat("speed", Mathf.Abs(move));
            myRB.velocity = new Vector3(move * data.runSpeed, myRB.velocity.y, 0);

            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
            bool puncher = takeAction("Punch", "grounded", true, myAnim);

            if (puncher)
            {
                StartCoroutine(HitStopperPunch());
            }
            bool slammer = comboAction("Punch", "Slam", "grounded", false, myAnim);

            if (slammer)
            {
                myAnim.SetBool("airborne", false);
                myAnim.SetBool("slam", true);
            }
            bool jumper = takeAction("Jump", "grounded", true, myAnim);

            if (jumper)
            {
                Jump();
            }
            if (Mathf.Abs(myRB.velocity.y) == 0)
            {
                myAnim.SetBool("grounded", true);
                myAnim.SetBool("airborne", false);
            }
        }


        else if (undead)
        {
            reSpawn();
            myHealth.currentHP = PlayerHealth.maxHP;
        }


    }

    /// <summary>
    /// Returns bool based on the button press, and animState ==flag
    /// </summary>
    public static bool ActionTook(String button, String state, bool flag, Animator myAnim)
    {
        bool acted;
        acted = (Input.GetButton(button) && myAnim.GetBool(state) == flag) ? true : false;
        return acted;
    }

    /// <summary>
    /// Returns bool based on dual buttons pressed and AnimState ==flag
    /// </summary>
    public static bool ComboActionTake(String button1, String button2, String state, bool flag, Animator myAnim)
    {
        bool acted;
        acted = (Input.GetButton(button1) && Input.GetButton(button2) && myAnim.GetBool(state) == flag);
        return acted;
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

    public virtual void Jump()
    {
        myAnim.SetBool("grounded", false);
        myAnim.SetBool("airborne", true);
        myRB.velocity = new Vector3(myRB.velocity.x, data.jumpHeight, 0);
    }

    //false respawn, resets transposition
    public virtual void reSpawn()
    {
        transform.SetPositionAndRotation(respawnPos, rot);
        myAnim.SetBool("dead", false);
        myAnim.SetBool("grounded", true);
        facingRight = true;
        myHealth.dead = false;
    }

    public void die()
    {
        transform.Rotate(0, 0, 90);
        myAnim.SetBool("dead", true);
    }

    //hitStop coroutine for punch to hold and then stop animation
    IEnumerator HitStopperPunch()
    {
        myAnim.SetBool("punching", true);
        //find better way to hitStop on the punch its jaggy atm
        yield return new WaitForSeconds(myAnim.GetCurrentAnimatorStateInfo(0).length / 5f);
        //stops movement while punch is animated restarts on end
        myAnim.SetBool("punching", false);
    }

    //declares slam bool false upon ground contact resetting its anim state
    void OnCollisionEnter(Collision collision)
    {
        myAnim.SetBool("slam", false);
        myAnim.SetBool("airborne", false);
        myAnim.SetBool("grounded", true);

        if (collision.gameObject.tag == "Ground")
        {
            myAnim.SetBool("grounded", true);
            myAnim.SetBool("airborne", false);
        }

        else if (collision.gameObject.tag == "Death Object")
        {
            die();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (Mathf.Abs(myRB.velocity.y) > 0)
        {
            myAnim.SetBool("grounded", false);
            myAnim.SetBool("airborne", true);
        }
    }
}


