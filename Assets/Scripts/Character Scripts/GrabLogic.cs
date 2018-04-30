using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Component that handles the collision logic of the grab hand
/// </summary>
[RequireComponent (typeof (GrabModel))]
public class GrabLogic : MonoBehaviour {
    GrabModel grabModel;

    SphereCollider handCollider;    //may have to change once final model is implemented
    int damage;

	// Use this for initialization
	void Start () {

		if (handCollider != null)
        {
            return;
        }
        else
        {
            handCollider = this.GetComponent<SphereCollider>();
        }

        if (grabModel != null)
        {
            return;
        }
        else
        {
            grabModel = gameObject.GetComponent<GrabModel>();
        }

        grabModel.On_DamageAtHand_Sent += On_DamageAtHand_Received;

    }

    /// <summary>
    /// The damage the hand does is broadcast/assigned to global variable
    /// of this class in this method
    /// </summary>
    /// <param name="dmg"></param>
    private void On_DamageAtHand_Received(int dmg)
    {
        damage = dmg;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (handCollider.enabled)
        {
            GrabDetection();
        }
    }

    /// <summary>
    /// Fixes the 
    /// </summary>
    void GrabDetection()
    {
        Collider[] cols = Physics.OverlapSphere(handCollider.bounds.center, .6f, LayerMask.GetMask("Enemy"));
        foreach (Collider enemyCols in cols)
        {
            if (enemyCols.tag == "HurtBox")
            {
                Enemy target = enemyCols.gameObject.transform.parent.gameObject.GetComponent<Enemy>();
                if (target.HP <= target.saveHP / 2)
                {
                    target.BecomeProjectile();
                }
                else
                    Debug.Log("Hand hurt enemy");
                    target.EnemyTakeDamage(damage, "Hand");
            }
        }
    }
}
#region TODO list, refactoring etc
/************TODO Refactoring********************************************************************//*
 1-Make it work for the final version of Sparx
 2-
 3-
 4-
 *************************************************************************************************/
#endregion
