using System;
using UnityEngine;
/// <summary>
/// Class used to control the TrashCannon cone of vision and some of its logic
/// </summary>
public class EnemyConeOfVision : MonoBehaviour {

    public Animator myAnim;
    float direction = 1;

    [Tooltip("Float that determines the distance considered to be close quarters for melee attack")]
    [Range(0f, 10f)]
    public float distance = 2f;

    private void Start()
    {
        Physics.IgnoreLayerCollision(0, 13,true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("OnTriggerEnter of EnemyConeOfVision {0}, tagged-object", other.gameObject.tag));
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player seen");
            myAnim.SetBool("enemyDetected",true);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    //Debug.Log(string.Format("OnTriggerStay of EnemyConeOfVision {0}, tagged-object", other.gameObject.tag));
    //    if (other.gameObject.tag == "Player")
    //    {
    //        myAnim.SetBool("enemyDetected", true);
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(string.Format("OnTriggerExit of EnemyConeOfVision {0}, tagged-object", other.gameObject.tag));
        if (other.gameObject.tag == "Player")
        {
            myAnim.SetBool("enemyDetected",false);
        }
    }
}
