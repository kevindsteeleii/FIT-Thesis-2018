using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    public int HP { get; private set; }
    public float RateOfFire { get; private set; }
    public event Action On_EnemyHPZero_Sent;

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void Update()
    {
        if (On_EnemyHPZero_Sent != null && HP <= 0)
        {
            On_EnemyHPZero_Sent();
        }
    }
    /// <summary>
    /// Calculates damage.
    /// </summary>
    /// <param name="dam"></param>
    
}
