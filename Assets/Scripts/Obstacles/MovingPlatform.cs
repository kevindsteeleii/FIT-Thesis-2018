using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum MoveDirection { Horizontal, Vertical };
public class MovingPlatform : MonoBehaviour
{
    Rigidbody myRb;
    [Range(0, 5)]
    public float dist = 0.2f;   //distance from the borders of an oscillation
    [Range(10,100)]
    public float speed = 10f;
    Ray ray;
    public MoveDirection direction = MoveDirection.Vertical;
    Vector3 vecDirection = Vector3.up;
    int polarity = 1;

    // Use this for initialization
    void Start()
    {
        if (myRb != null)
        {
            return;
        }
        else
        {
            myRb = gameObject.GetComponent<Rigidbody>();
        }

        if (direction == MoveDirection.Vertical)
        {
            vecDirection = Vector3.up;
            
            Now = VerticalMover;
        }
        else
        {
            vecDirection = Vector3.right;
            ray = new Ray(transform.position, vecDirection);
            Now = HorizontalMover;
        }
    }
    public delegate void Go();
    Go Now;

    void VerticalMover()
    {
        ray = new Ray();

        ray = (myRb.velocity.y >= 0) ? new Ray(transform.position, Vector3.up): new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, dist))
        {
            if (hit.collider.tag  == "EndOfLine")
            {
                Debug.Log("Hit the Vertical");
                TurnBackDown();
            }
        }
        myRb.velocity = vecDirection * Time.timeScale * speed * polarity;
    }

    void HorizontalMover()
    {
        //ray = new Ray();
        //ray = (myRb.velocity.x >= 0) ? new Ray(transform.position, Vector3.right) : new Ray(transform.position, Vector3.left);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, dist))
        {
            if (hit.collider.tag == "EndOfLine")
            {
                Debug.Log(String.Format("Hit the edge of horizontal with dist ={0} with polarity at {1}",dist,polarity));
                TurnBack();
            }
        }
        //myRb.velocity = vecDirection * Time.timeScale * speed * polarity;
        Vector3 horMove = transform.position;
        horMove.x += vecDirection.x * Time.timeScale * speed * polarity;
        transform.position = horMove;
    }

    void TurnBackDown()
    {
        polarity *= -1;
    }

    void TurnBack()
    {
        polarity *= -1;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Now();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.localPosition = Vector3.MoveTowards(other.gameObject.transform.localPosition, transform.position, 2f);
        }
    }
}
