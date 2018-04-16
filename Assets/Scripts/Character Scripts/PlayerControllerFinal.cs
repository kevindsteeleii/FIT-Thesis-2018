﻿using System;
using System.Collections;
using UnityEngine;

public class PlayerControllerFinal : Singleton<PlayerControllerFinal>
{

    #region Global Variables
    //set of variables used to detect if the ground is beneath or not
    public PlayerData data;
    Ray ray = new Ray(); // ray used to detect if player is over a ground object wh/ is used to affect camera movements
    bool groundBeneath = false; //bool that is used to keep track of whether the ground is infact beneath the player character
    GameObject targetCamera; //camera used to subscribe to the On_GroundRayCasting_Sent event to change the camera's verticality

    //rigidboday and animator children of the player character 
    Animator myAnim;
    Rigidbody myRB;
    public static bool facingRight;


    [Tooltip("The punch chain time interval used to work")]
    [Range(0.1f, 4f)]
    public float comboInterval;
    float lastPressed = 0f;
    int buttonPresses = 0;  //used to increase the combo 

    //respawn position
    Vector3 respawnPos;
    //respawn rotation
    Quaternion rot;

    /// <summary>
    /// Event that broadcasts the current location of the player character
    /// </summary>
    public event Action<Vector3> On_PlayerPosition_Sent;

    /// <summary>
    /// keeps position to be referred to outside
    /// </summary>
    public Vector3 myPos;

    /// <summary>
    /// Event that broadcasts whether the player character is below the ground or not to affect camera vertical movement
    /// </summary>
    public event Action<bool> On_GroundRayCasting_Sent;

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
    #endregion

    //establishes defaults and initiates variables/placeholders use Start over Awake for all singletons
    protected virtual void Start()
    {
        myAnim = this.GetComponent<Animator>();
        myRB = this.GetComponent<Rigidbody>();
        facingRight = true;
        //myAnim.SetBool("grounded", false);
        respawnPos = myRB.transform.position;
        rot = myRB.transform.rotation;
        targetCamera = GameObject.FindGameObjectWithTag("MainCamera");

        //assigns ResetHP() as subscriber of Restarting event
        GameManager.instance.On_RestartState_Sent += On_ReStartState_Caught;
        On_GroundRayCasting_Sent += targetCamera.GetComponent<CameraFollowv2_0>().On_GroundRayCasting_Received;
    }

    protected virtual void Update()
    {
        myPos = transform.position;
        if (On_PlayerPosition_Sent != null)
        {
            On_PlayerPosition_Sent(myPos);
        }

        //sends whether or not the character is underneath ground
        if (On_GroundRayCasting_Sent != null)
        {
            On_GroundRayCasting_Sent(groundBeneath);
        }

        if (GameManager.instance.GetState() == GameState.inGame)
        {
            if (Input.GetButtonDown("Punch") && myAnim.GetBool("grounded"))
            {
                myAnim.SetFloat("speed", 0f);
                lastPressed = Time.time;
                //Debug.Log("Last pressed punch at " + lastPressed);
                myAnim.SetBool("attacking", true);

                if (buttonPresses <= 0)
                {
                    buttonPresses++;

                }
                else
                {
                    if (Input.GetButtonDown("Punch") && Time.time <= (lastPressed + comboInterval))
                    {
                        myAnim.SetFloat("speed", 0f);
                        buttonPresses++;

                    }
                    else if (myAnim.GetInteger("attackChain") >= 4)
                    {
                        myAnim.SetInteger("attackChain", 0);
                        buttonPresses = 0;
                    }
                }

            }
            //else
            //{
            //    myAnim.SetBool("attacking", false);
            //}

            if (Time.time > (lastPressed + comboInterval) || myAnim.GetInteger("attackChain") >= 4)
            {
                buttonPresses = 0;
                myAnim.SetBool("attacking", false);
            }
            //myAnim.SetBool("attacking", false);

            if (Input.GetAxisRaw("JoystickVertical") == -1 //&& Input.GetButton("Punch") 
                && !myAnim.GetBool("grounded"))
            {
                Debug.Log("welcome to the slam");
                myAnim.SetBool("slam", true);
            }
            else
                myAnim.SetBool("slam", false);

            //this is the alternative inputs for the slam on controller
            //if (Input.GetAxisRaw("JoystickVertical") == -1 && Input.GetButton("Punch"))
            //{
            //    //myAnim.SetBool("airborne", false);
            //    myAnim.SetBool("slam", true);
            //}
            //else
            //{
            //    myAnim.SetBool("slam", false);
            //}
        }
    }

    // Update is called once per physics action
    protected virtual void FixedUpdate()
    {
        #region Logic for the Raycast detecting ground underneath to figure out if camera needs to change verticality
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Ground")
            {
                groundBeneath = true;
            }
            else
            {
                groundBeneath = false;
            }
        }
        #endregion

        //bool undead = takeAction("Respawn", "dead", true, myAnim);
        //if false death-state is on all other actions cease

        if (GameManager.instance.GetState() == GameState.inGame)
        {
            #region Horizontal Movement
            float move = 0;
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                move = Input.GetAxis("Horizontal");
            }

            myAnim.SetFloat("speed", Mathf.Abs(move));

            myRB.velocity = new Vector3(move * data.runSpeed, myRB.velocity.y, 0);
            if (move > 0 && !facingRight) { Flip(); }
            else if (move < 0 && facingRight) { Flip(); }
            #endregion
            //Debug.Log(buttonPresses + " button presses");

            myAnim.SetInteger("attackChain", buttonPresses);
        }
    }

    /// <summary>
    /// Returns bool based on the button press, and animState ==flag
    /// </summary>
    protected static bool ActionTook(String button, String state, bool flag, Animator myAnim)
    {
        bool acted;
        acted = (Input.GetButtonDown(button) && myAnim.GetBool(state) == flag) ? true : false;
        return acted;
    }

    /// <summary>
    /// Returns bool based on dual buttons pressed and AnimState ==flag
    /// </summary>
    protected static bool ComboActionTake(String button1, String button2, String state, bool flag, Animator myAnim)
    {
        bool acted;
        acted = (Input.GetButton(button1) && Input.GetButton(button2) && myAnim.GetBool(state) == flag);
        return acted;
    }

    //flips character when velocity changes from positive to negative and facing the wrong way
    protected virtual void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

    //respawns transform and animation info
    protected virtual void On_ReStartState_Caught()
    {
        transform.SetPositionAndRotation(respawnPos, rot);
        myAnim.SetBool("dead", false);
        //myAnim.SetBool("grounded", true);
        facingRight = true;
    }

    protected virtual void Die()
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
        //myAnim.SetBool("slam", false);
        //myAnim.SetBool("airborne", false);
        //myAnim.SetBool("grounded", true);

        //if (collision.gameObject.tag == "Ground")
        //{
        //    myAnim.SetBool("grounded", true);
        //    myAnim.SetBool("Jump", false);
        //    myAnim.SetBool("Falling", false);
        //}

        if (collision.gameObject.tag == "Death Object")
        {
            Die();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            respawnPos.x = other.gameObject.transform.position.x;
            respawnPos.z = 0;
        }
    }
}
#region TODO list, refactoring etc
/************TODO Refactoring********************************************************************//*
 1- fine tune the combo animations/timing to be more reactive to the button presses
 2-
 3-
 4-
 *************************************************************************************************/
#endregion