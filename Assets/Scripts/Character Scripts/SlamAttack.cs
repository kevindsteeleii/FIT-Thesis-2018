using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component used to create the slam based on its animation state
/// </summary>
public class SlamAttack : BaseAttack {

    [SerializeField]
    SphereCollider slamArea;

    [SerializeField]
    Animator myAnim;

	// Use this for initialization
	void Start () {
		if (slamArea == null)   {
            SphereCollider[] allSpheres = GameObject.FindObjectsOfType<SphereCollider>();
            foreach (SphereCollider sphere in allSpheres)  {
                if (sphere.name == "SlamHitBox")    {
                    slamArea = sphere;
                }
                else
                    continue;
            }
        }

        if (myAnim == null) {
            myAnim = gameObject.GetComponent<Animator>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        float normalTime = myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime; //returns the percentage of completion of animation as a float
        slamArea.enabled = myAnim.GetBool("slam"); //bool is determined by when/during the punch animation is active/on

        if (slamArea.enabled && normalTime > 0.2f)  {
            //WelcomeToTheSlam();
            AttackDetection();
        }
    }

    public override void AttackDetection()
    {
        Collider[] cols = Physics.OverlapSphere(slamArea.bounds.center, slamArea.radius, LayerMask.GetMask("Enemy"), QueryTriggerInteraction.Collide);
        EnemyHit(cols);
    }
}
