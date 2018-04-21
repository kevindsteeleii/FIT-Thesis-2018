using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanon_CollisionBasic : MonoBehaviour {

    Animator enAnim;
    Rigidbody myRB;

    public void Start()
    {
        if (enAnim != null)
        {
            return;
        }
        else
        {
            enAnim = gameObject.GetComponentInChildren<Animator>();
        }

        if (myRB != null)
        {
            return;
        }
        else
        {
            myRB = gameObject.GetComponentInParent<Rigidbody>();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collided with " + collision.gameObject.tag);

        if (collision.gameObject.tag == "Ground")
        {
            enAnim.SetBool("grounded", true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {

    }
}
