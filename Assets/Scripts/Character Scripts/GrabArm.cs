using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabArm : BaseAttack {
    SphereCollider grabCollider;
    Animator myAnim;
    bool grabbing;

    [Tooltip("The amount of damage the hand causes")]
    [Range(0, 15)]
    public int damage = 10;

	// Use this for initialization
	void Start () {
		if (grabCollider != null)
        {
            return;
        }
        else
        {
            grabCollider = gameObject.GetComponent<SphereCollider>();
            grabCollider.enabled = false;
        }

        if (myAnim != null)
        {
            return;
        }
        else
        {
            myAnim = gameObject.transform.root.gameObject.GetComponent<Animator>();
        }
	}

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (grabCollider.enabled)
        {
            GrabDetection();
        }
    }

    public void On_Grabbing_Received(bool isGrabbing)
    {
        grabCollider.enabled = isGrabbing;
    }

    /// <summary>
    /// Used to detect whether the had grabbed
    /// </summary>
    void GrabDetection()
    {
        Collider[] cols = Physics.OverlapSphere(grabCollider.bounds.center, .6f, LayerMask.GetMask("Enemy"));
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
                target.EnemyTakeDamage(damage,"Hand");
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
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