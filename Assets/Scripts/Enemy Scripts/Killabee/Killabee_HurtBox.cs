using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killabee_HurtBox : MonoBehaviour {
    public Enemy me;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public virtual void BecomeProjectile()
    {
        me.On_BecomeAmmo_Sent += Ammo.instance.Load;
        //transmits to Ammo handling manager as a subject of subscription
        //if (me.On_BecomeAmmo_Sent != null)
        //{
        //    Debug.Log("Became Ammo");
        //    me.On_BecomeAmmo_Sent();
        //}
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand")
        {
            me.EnemyTakeDamage(other.gameObject.GetComponent<GrabModel>().damage);

            if (me.HP <= me.saveHP / 2)
            {
                BecomeProjectile();
            }
        }
    }
}
