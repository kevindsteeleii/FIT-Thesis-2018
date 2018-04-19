using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamAttackFinal : BaseAttack {

    [SerializeField]
    SphereCollider slamCollider;

    [SerializeField]
    Animator myAnim;

    // Use this for initialization
    void Start() {
        if (slamCollider == null)
        {
            slamCollider = gameObject.GetComponent<SphereCollider>();
        }

        if (myAnim == null)
        {
            myAnim = gameObject.GetComponentInParent<Animator>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        slamCollider.enabled = myAnim.GetBool("slam");

	}
}
