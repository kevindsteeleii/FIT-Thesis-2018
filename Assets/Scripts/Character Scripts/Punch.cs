using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component used to create the punch based on the animation state parameters
/// </summary>
public class Punch : BaseAttack {

    [SerializeField]
    CapsuleCollider punchCapsule;

    [SerializeField]
    Animator myAnim;

    // Use this for initialization
    void Start () {
		if (punchCapsule == null)
        {
            punchCapsule = this.gameObject.GetComponent<CapsuleCollider>();
        }

        if (myAnim == null)
        {
            myAnim = gameObject.GetComponentInParent<Animator>();
        }
	}

	// Update is called once per frame
	void Update () {
        float normalTime = myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime; //returns the percentage of completion of animation as a float
        
        punchCapsule.enabled = myAnim.GetBool("punching"); //bool is determined by when/during the punch animation is active/on
        if (punchCapsule.enabled && normalTime > 0.2f )
        {
            int baseName = myAnim.GetCurrentAnimatorClipInfo(0).Rank;

            AttackDetection();
        }
    }

    public override void AttackDetection()
    {
        Collider[] cols = Physics.OverlapCapsule(punchCapsule.bounds.center, punchCapsule.bounds.extents, punchCapsule.radius, LayerMask.GetMask("Enemy"));
        EnemyHit(cols);
    }
}
