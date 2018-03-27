using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestCollisions : MonoBehaviour {

    public BoxCollider hurtBox;

	// Use this for initialization
	void Start () {
		if (hurtBox == null)
        {
            hurtBox = gameObject.GetComponent<BoxCollider>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

    }
}