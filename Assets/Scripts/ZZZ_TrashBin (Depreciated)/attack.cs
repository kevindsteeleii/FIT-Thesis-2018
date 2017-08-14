using System.Collections;
using UnityEngine;

/// <summary>
/// needs complete overhaul as a new more organized class, still to be kept for reference for logic 
/// but ultimately very messy and handles input poorly as there is no input hierarchy which needs to be
/// resolved post-haste
/// </summary>
public class attack : MonoBehaviour
{

    // Use this for initialization
    Animator myAnim;

    [Range(0, 2)]
    public float hitStop = 0.4f;

    [SerializeField]
    GameObject player;

    protected virtual void Awake()
    {

        player = this.gameObject;
        myAnim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {

        //triggers punch animation while K is pressed
        if (Input.GetButton("Punch") && myAnim.GetBool("grounded"))
        {

            StartCoroutine(HitStopperPunch());
        }

        if (Slamming() && myAnim.GetBool("grounded") == false)
        {
            myAnim.SetBool("airborne", false);
            myAnim.SetBool("slam", true);
        }
    }

    //declares slam bool false upon ground contact resetting its anim state
    void OnCollisionEnter(Collision collision)
    {
        myAnim.SetBool("slam", false);
        myAnim.SetBool("airborne", false);
        myAnim.SetBool("grounded",true);
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Enemy"))  {
            //**faux code no declaration or creation of a Damage/Health class as of yet
            //**Damage.applyDamage(float damageAmout);
        }
    }

    //fixes the slam logic for button combination
    bool Slamming()    {
        if (Input.GetButton("Punch") && Input.GetButton("Slam"))
            return true;
        else
            return false;
    }

    //hitStop coroutine for punch to hold and then stop animation
    IEnumerator HitStopperPunch()    {
        myAnim.SetBool("punching", true);
        //find better way to hitStop on the punch its jaggy atm
        yield return new WaitForSeconds(myAnim.GetCurrentAnimatorStateInfo(0).length / 5f);
        //stops movement while punch is animated restarts on end
        myAnim.SetBool("punching", false);
    }

}
