﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisionDetection : MonoBehaviour
{

    Rigidbody myRb;
    MeshCollider coneCollider;
    // Use this for initialization
    void Start()
    {
        if (myRb == null)
        {
            var parent = gameObject.transform.parent;
            myRb = parent.gameObject.GetComponent<Rigidbody>();
        }

        if (coneCollider == null)
        {
            coneCollider = gameObject.GetComponentInChildren<MeshCollider>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rotation = 0f;
        if (myRb.velocity.x > 0)
        {
            rotation = 0;
        }
        else if (myRb.velocity.x < 0)
        {
            rotation = 180f;
        }
        gameObject.transform.rotation = new Quaternion(Vector3.up.x, Vector3.up.y * rotation, Vector3.up.z, 1);
    }
}
