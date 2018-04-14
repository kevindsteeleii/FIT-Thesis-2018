using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skirt : MonoBehaviour {
    public Transform belt;

    [Tooltip("The vertical offset used for the battle skirt")]
    [Range(-2f, 2f)]
    public float yOffset = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = belt.position;
        pos.y = belt.position.y + yOffset;
        gameObject.transform.position = pos;
	}
}
