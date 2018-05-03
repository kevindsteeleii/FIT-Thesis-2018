using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingPlatformv2_0 : MonoBehaviour {

    //[Range(0, 5)]
    //public float dist = 0.2f;   //distance from the borders of an oscillation
    [Range(0.1f, 4)]
    public float speedMultiplier = 2;
    ///*public*/ MoveDirection direction = MoveDirection.Vertical;
    //Vector3 vecDirection = Vector3.up;
    public GameObject post1, post2;
    float progress = 0.0f;
    float multiplier = 1.0f;

    // Use this for initialization
    void Start () {
        post1 = gameObject.transform.root.GetChild(0).gameObject;   //comment out if the posts aren't in the same parent object as the platform itself
        post2 = gameObject.transform.root.GetChild(1).gameObject;   //comment out if the posts aren't in the same parent object as the platform itself
        StartCoroutine("MakeProgress");
    }

    IEnumerator MakeProgress()
    {
        int progressMultiplier = 1;

        progress = 0f;

        while(true)
        {
            progress += Time.smoothDeltaTime * speedMultiplier * progressMultiplier;

            if (progressMultiplier > 0)
            {
                if (progress >= 1.0f)
                {
                    progress = 1.0f;
                    progressMultiplier *= -1;
                }
            }
            else
            {
                if (progress <= 0.0f)
                {
                    progress = 0.0f;
                    progressMultiplier *= -1;
                }
            }
            transform.position = Vector3.Lerp(post1.transform.position, post2.transform.position, progress);
            yield return null;
        }
        
    }
}
