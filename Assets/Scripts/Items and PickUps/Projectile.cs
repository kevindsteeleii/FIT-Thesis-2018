using UnityEngine;

/// <summary>
/// Class that houses logic for Projectile
/// </summary>
public class Projectile : Model
{
    [SerializeField]
    GameObject rootAim;

    SphereCollider hitDetector;


    //damage the projectile causes
    [Range (0,15)]
    public int damage;

    private void Start()
    {
        if (rootAim == null)
        {
            rootAim = GameObject.FindGameObjectWithTag("RootAim");
        }
        if (hitDetector == null)
        {
            hitDetector = gameObject.GetComponent<SphereCollider>();
        }
    }

    private void Update()
    {
        Shooting();
    }

    private void OnBecameInvisible()
    {
        Destroy();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Projectile destroyed!!");
        }
        //Destroy();
    }

    void Shooting()
    {
        Collider[] cols = Physics.OverlapSphere(hitDetector.bounds.center, hitDetector.radius, LayerMask.GetMask("Enemy"),QueryTriggerInteraction.Collide);
        {
            foreach (Collider col in cols)
            {
                if (col.gameObject.tag == "Enemy")
                {
                    Debug.Log("Enemy hit!!!");
                    //Destroy();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy hit!!!");
            Destroy();
        }
    }
    //Doesn't "destroy" as much as set's active to false and rejoins it's old parent object in hierarchy
    public void Destroy()
    {
        //gameObject.transform.SetParent(rootAim.transform);
        gameObject.SetActive(false);
    }
}
