using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanon_CollisionBasic : MonoBehaviour {
    Animator enAnim;
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
        //Debug.Log("Triggered by "+other.gameObject.tag);
    }
}
