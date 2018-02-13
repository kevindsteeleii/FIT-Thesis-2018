using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component used to create the slam based on its animation state
/// </summary>
public class SlamAttack : MonoBehaviour {

    [SerializeField]
    SphereCollider slamArea;

    [SerializeField]
    Animator myAnim;

    [Tooltip("The damage the punch inflicts upon an enemy or destructible obstacle")]
    [Range(0, 20)]
    public int dam = 10;

	// Use this for initialization
	void Start () {
		if (slamArea == null)
        {
            SphereCollider[] allSpheres = GameObject.FindObjectsOfType<SphereCollider>();
            foreach (SphereCollider sphere in allSpheres)
            {
                if (sphere.name == "SlamHitBox")
                {
                    slamArea = sphere;
                }
                else
                    continue;
            }
        }

        if (myAnim == null)
        {
            myAnim = gameObject.GetComponent<Animator>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        float normalTime = myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime; //returns the percentage of completion of animation as a float
        slamArea.enabled = myAnim.GetBool("slam"); //bool is determined by when/during the punch animation is active/on
        if (slamArea.enabled && normalTime > 0.2f)
        {
            Debug.Log("Slam detected!!");
            WelcomeToTheSlam();
        }
    }

    void WelcomeToTheSlam()
    {
        Collider[] cols = Physics.OverlapSphere(slamArea.bounds.center, slamArea.radius, LayerMask.GetMask("Enemy"), QueryTriggerInteraction.Collide);
        foreach (Collider col in cols)
        {
            if (col.tag == "HurtBox")
            {
                Debug.Log("Enemy hit");
                col.gameObject.GetComponentInParent<Enemy>().TakeDamage(dam);
            }
        }
    }
}
