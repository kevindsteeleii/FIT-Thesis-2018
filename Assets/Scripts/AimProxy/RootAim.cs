using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to follow player character w/o problematic rotations
/// or other adjustments to models etc.
/// Works as inteded do not directly adjust or change
/// </summary>
public class RootAim : MonoBehaviour
{
    //player as target
    [SerializeField]
    private PlayerController target;
    private bool facesRight;


    public virtual void Awake()
    {
        facesRight = true;
        

    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {

        Vector3 desiredPosition = target.transform.position;
        desiredPosition.z = 0f;
        transform.position = desiredPosition;

        float move = Input.GetAxis("Horizontal");
        if (move > 0 && !facesRight)
        {
            flipMe();
        }
        else if (move < 0 && facesRight)
        {
            flipMe();
        }

    }

    public virtual bool RightorLeft()
    {
        return true;
    }

    public virtual void flipMe()
    {
        //Debug.Log("flipping");
        facesRight = !facesRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }
}

