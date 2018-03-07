using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public enum Direction { left = -1, right = 1 };
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

	// Update is called once per frame
	void Update () {
        float multiplier = 0;
        multiplier = (myDirection == Direction.left) ? -1 : 1;
        Vector3 adjust = followObject.position;
        float xOff = adjust.x + (xOffset * multiplier);
        adjust.x = xOff;
        myCollider.transform.position = adjust;
    }
}
