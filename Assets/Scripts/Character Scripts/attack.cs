using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{

    // Use this for initialization
    Animator myAnim;
    Rigidbody myRB;

    [Range(0, 2)]
    public float hitStop = 0.4f;

    [SerializeField]
    GameObject player;
    

    protected virtual void Awake()
    {

        player = this.gameObject;
        myAnim = player.GetComponent<Animator>();
        myRB = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        //triggers punch animation while K is pressed
        if (Input.GetButtonDown("Punch") && myAnim.GetBool("grounded") == true)
        {
            StartCoroutine(HitStopperPunch());
        }

        //if L is pressed in the air, cancels other animations and starts the slam animation
        if (myAnim.GetBool("grounded") == false && Input.GetButton("Slam"))
        {
            myAnim.SetBool("slam", true);
        }
    }

    //declares slam bool false upon ground contact resetting its anim state
    void OnCollisionEnter(Collision collision)
    {
        myAnim.SetBool("slam", false);
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Enemy"))
        {
            //**faux code no declaration or creation of a Damage/Health class as of yet
            //**Damage.applyDamage(float damageAmout);
        }
    }

    //hitStop coroutine for punch to hold and then stop animation
    IEnumerator HitStopperPunch()
    {
        myAnim.SetBool("punching", true);
        player.BroadcastMessage("stopMoving");
        //find better way to hitStop on the punch its jaggy atm
        yield return new WaitForSeconds(hitStop);
        //stops movement while punch is animated restarts on end
        myAnim.SetBool("punching", false);
        player.BroadcastMessage("startMoving");
    }
}
