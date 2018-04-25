using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtBox : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBox" && other.gameObject.layer == 9)
        {
            other.gameObject.transform.root.gameObject.GetComponent<PunchFinal>().On_TransferDamage_Sent +=gameObject.transform.parent.gameObject.GetComponent<Enemy>().EnemyTakeDamage;
            other.gameObject.transform.root.gameObject.GetComponent<PunchFinal>().On_TransferDamage_Sent -= gameObject.transform.parent.gameObject.GetComponent<Enemy>().EnemyTakeDamage;
            other.gameObject.transform.root.gameObject.GetComponent<SlamAttackFinal>().On_TransferDamage_Sent += gameObject.transform.parent.gameObject.GetComponent<Enemy>().EnemyTakeDamage;
            other.gameObject.transform.root.gameObject.GetComponent<SlamAttackFinal>().On_TransferDamage_Sent -= gameObject.transform.parent.gameObject.GetComponent<Enemy>().EnemyTakeDamage;
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
