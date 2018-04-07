using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public enum Direction { left = -1, right = 1 , top = 0};
/// <summary>
/// This component is used for a box collider that determines when ammo is no longer viable based upon
/// its absence from a box collider
/// </summary>
///
public class ScreenBarier : MonoBehaviour {
    [SerializeField]
    Transform followObject;
    [SerializeField]
    BoxCollider myCollider;
    
    public Direction myDirection = Direction.left;

    [Tooltip("The horizontal offSet for the bullet to disappear after")]
    [Range(0, 20f)]
    public float xOffset;

    [Tooltip("The vertical offSet for the bullet to disappear after")]
    [Range(0, 20f)]
    public float yOffset;
    
    // Update is called once per frame
    void Update () {
        float multiplier = 0;
        Vector3 adjust = followObject.position;

        if (myDirection == Direction.left || myDirection == Direction.right)
        {
            multiplier = (myDirection == Direction.left) ? -1 : 1;

            float xOff = adjust.x + (xOffset * multiplier);
            adjust.x = xOff;
        }
        else if (myDirection == Direction.top)
        {
            float yOff = adjust.y + yOffset;
            adjust.y = yOff;
        }
        myCollider.transform.position = adjust;
    }
}
