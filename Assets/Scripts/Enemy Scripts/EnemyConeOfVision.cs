using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class used to control the TrashCannon cone of visions
/// </summary>
public class EnemyConeOfVision : MonoBehaviour {

    public Animator myAnim;

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
