using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowv2_0 : MonoBehaviour {

    //player as target
    public Transform target;
    //distance b/n camera and player
    public Vector3 offset;

    private Vector3 camRespawnPos;
    private Quaternion camRespawnAngle;

    public float smoothSpeed = 0.125f;

    void Awake()
    {
        //joffset = Vector3.forward * -7.5f;
    }

    protected void Start()
    {
        camRespawnPos = this.transform.position;
        camRespawnAngle = this.transform.rotation;

        if (target == null)
        {
            //if target is null/ in other scene then find object with the tag player
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    /// <summary>
    /// Resets the camera to default postion
    /// </summary>
    protected void ResetCam()
    {
        this.transform.position = camRespawnPos;
        this.transform.rotation = camRespawnAngle;
    }
    // Update is called once per frame after Update
    void FixedUpdate()
    {
        //Vector3 desiredPosition = target.position + offset;
        Vector3 desiredPosition = this.transform.position;
        float tempX = target.position.x+ offset.x;
        float tempY = offset.y;
        desiredPosition.z = offset.z;
        desiredPosition.x = tempX;
        desiredPosition.y = tempY;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
