using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// replace this entire class functionality with
// physics velocity, it's faster and is less code...
public class EnemyPace : MonoBehaviour
{
    [Range(0, 2)]
    public float speed;
    [SerializeField]
    Rigidbody rb;
    //time it takes to move across
    [Range(0, 3)]
    public float delta = 1.5f;

    // Use this for initialization
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
        rb.velocity = new Vector3(delta * Mathf.Sin(Time.time * speed), 0, 0);
    }
}
