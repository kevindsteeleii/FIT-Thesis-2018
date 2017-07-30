using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidAirAttack : MonoBehaviour {

    /// <summary>
    /// player object to be acted upon
    /// </summary>
    [SerializeField]
    private GameObject player;

    private Animator myAnim;
    private Rigidbody myRB;

    private bool isGrounded;
    

    //range slider of time for hitstop midair
    [Range(0f, 2f)]
    public float hitStopAir;

    // Use this for initialization
    protected virtual void Awake()    {
        //insures that if not specified player is chosen as the object this script is attached to
        if (!player)        {
            player = this.gameObject;
        }
        myAnim = player.GetComponent<Animator>();
        myRB = player.GetComponent<Rigidbody>();
        hitStopAir = 0.3f;
    }
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
        if (!myAnim.GetBool("dead")) {
            //triggers midair attack and subsequent hang time
            if (Input.GetButton("Punch") && !isGrounded) {
                StartCoroutine(midAirHitStop());
            }
        }
		
	}

    //Coroutine for midAir Hit Stop
    IEnumerator midAirHitStop () {
        //turns off physics of object for a quick sec to float the attack
        myRB.useGravity = false;
        Debug.Log("Freeze MidAir");
        yield return new WaitForSeconds(hitStopAir);
        //physics turned back on
        myRB.useGravity = true;
    }

    //on collision with thing known as ground sets to grounded
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
        else
            isGrounded = false;
        if (collision.gameObject.CompareTag("Enemy")) {
               //doDamage() probably in a broadcastMessage or something
               //need to still build death and 
        }
    }
}
