using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchFinal : BaseAttack {

    [SerializeField]
    Animator myAnim;
    // Use this for initialization
    void Start()
    {
        if (myHitBox == null)
        {
            myHitBox = this.gameObject.GetComponent<SphereCollider>();
        }

        if (myAnim == null)
        {
            myAnim = gameObject.GetComponentInParent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //float normalTime = myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime; //returns the percentage of completion of animation as a float

        myHitBox.enabled = myAnim.GetBool("attacking"); //bool is determined by when/during the punch animation is active/on
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
