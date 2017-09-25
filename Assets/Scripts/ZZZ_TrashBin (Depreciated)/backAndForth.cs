using UnityEngine;

// this looks like a copy of EnemySpeed.cs
// apply the changes I suggested to EnemySpeed,
// make it a component and then apply it to anything
// that needs that component, also rename EnemySpeed
// when you make it a component
public class backAndForth : MonoBehaviour
{

    [Range(0, 2)]
    public float speed;

    //time it takes to move across
    [Range(0, 3)]
    public float delta = 1.5f;

    private Vector3 startPos;


    // Use this for initialization
    void Awake()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = startPos;
        v.x += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
