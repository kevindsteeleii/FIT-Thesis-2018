using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Component Used to activate and the hitboxes attached to the same object as script and change the object's tag as well
/// </summary>
public class PlayerHitBoxControl : MonoBehaviour {
    public Animator myAnim;
    public Collider[] cols;
    /// <summary>
    /// Sends the tag to change the tag name of the subsequent chain of hitboxes/ reuses them as a result
    /// </summary>
    public event Action<string> On_ChangeTag_Sent;
	// Use this for initialization
	void Start () {

        foreach (Collider item in cols)
        {
            On_ChangeTag_Sent += item.gameObject.GetComponent<PlayerHitBox>().On_ChangeTag_Received;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (myAnim.GetBool("attacking") || myAnim.GetBool("slam"))
        {
            On_ChangeTag_Sent("HitBox");
            SetColliders(true);
        }
        else if(myAnim.GetBool("grabbing"))
        {
            On_ChangeTag_Sent("Hand");
            SetColliders(true);
        }
        else
        {
            SetColliders(false);
        }
    }

    void SetColliders(bool eval)
    {
        foreach (Collider item in cols)
        {
            item.gameObject.SetActive(eval);
        }
    }
}
    #region TODO list, refactoring etc
/************TODO Refactoring********************************************************************//*
 1- 
 2- 
 3-
 4-
 *************************************************************************************************/
#endregion