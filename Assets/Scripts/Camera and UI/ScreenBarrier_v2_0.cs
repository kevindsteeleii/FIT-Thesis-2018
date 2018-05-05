using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Uses new approach for the Screen barrier that should also affect the
/// enemy level of activity based on the relative distance from this activity wall
/// </summary>
[RequireComponent (typeof(BoxCollider))]    //attaches a box collider if none found
public class ScreenBarrier_v2_0 : MonoBehaviour {
    [SerializeField]
    Transform followObject;
    BoxCollider myCollider; //the literal bounding box of the active scene


	// Use this for initialization
	void Start () {
        gameObject.tag = "ActiveBox";   //sets the 

		if (followObject != null) 
        {
            return;
        }
        else
        {
            followObject = PlayerControllerFinal.instance.gameObject.transform.root.transform;
        }

        if (myCollider != null)
        {
            return;
        }
        else
        {
            myCollider = gameObject.GetComponent(typeof(BoxCollider)) as BoxCollider;
        }
	}

    // Update is called once per frame
    //void Update () {
    //       myCollider.transform.position = followObject.position;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ActiveBox" && other.gameObject.layer == 12)
        {
            other.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ActiveBox" && other.gameObject.layer == 12)
        {
            other.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ActiveBox" && other.gameObject.layer == 12)
        {
            other.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "ActiveBox" && collision.gameObject.layer == 12)
    //    {
    //        collision.gameObject.transform.GetChild(2).gameObject.SetActive(true);
    //    }
    //}
}
#region TODO list, refactoring etc
/************TODO Refactoring********************************************************************//*
 1- 
 2-
 3-
 4-
 *************************************************************************************************/
#endregion

