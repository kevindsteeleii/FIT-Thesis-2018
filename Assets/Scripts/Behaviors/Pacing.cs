using UnityEngine;

/// <summary>
/// Class used as a component to make objects, obstacles, and hovering enemies move back and forth on a set path horizontally
/// note: you need a rigid body for it to work and is designed to use it 
/// </summary>
[RequireComponent(typeof (Rigidbody))]
public class Pacing : MonoBehaviour
{
    //rigidbody used to move through velocity
    [SerializeField]
    Rigidbody rb;


    //speed of movement
    [Range(0, 20)]
    public float speed;

    //time it takes to move across
    [Range(0, 1000)]
    public float pathLength = 1.5f;

    //saves "default" position to force rigidBody back into its center of path after every subsequent sin wave cycle
    Vector3 pos;


    // Use this for initialization
    void Awake()
    {
        if (rb == null)
        {
            rb = this.GetComponent<Rigidbody>();
        }
        pos = this.transform.position;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(pathLength * Mathf.Sin(Time.time * speed), 0, 0);
        rb.transform.position = pos;
    }

}
