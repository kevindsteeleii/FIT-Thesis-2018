using System;
using UnityEngine;
/// <summary>
/// Class used to control the TrashCannon cone of vision and some of its logic
/// </summary>
public class EnemyConeOfVision : MonoBehaviour {

    public Animator myAnim;
    Rigidbody myRB;
    float direction = 1;

    [Tooltip("Float that determines the distance considered to be close quarters for melee attack")]
    [Range(0f, 10f)]
    public float distance = 2f;

    private void Start()
    {
        if (myRB != null)
        {
            return;
        }
        else
        {
            myRB = gameObject.GetComponentInParent<Rigidbody>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            myAnim.SetBool("enemyDetected",true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            myAnim.SetBool("enemyDetected", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            myAnim.SetBool("enemyDetected",false);
        }
    }
}
