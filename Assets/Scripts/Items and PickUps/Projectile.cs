using UnityEngine;

/// <summary>
/// Class that houses logic for Projectile
/// </summary>
public class Projectile : Model
{
    [SerializeField]
    GameObject rootAim;

    //damage the projectile causes
    [Range (0,15)]
    public int damage;

    public virtual void Start()
    {
        if (rootAim == null)
        {
            rootAim = GameObject.FindGameObjectWithTag("RootAim");
        }
    }

    private void OnBecameInvisible()
    {
        DestroyMe();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HurtBox" )
        {
            Debug.Log("Enemy hit on Trigger Projecile!!!");
            DestroyMe();
            //gameObject.SetActive(false);
        }
    }

    //Doesn't "destroy" as much as set's active to false and rejoins it's old parent object in hierarchy
    public void DestroyMe()
    {
        gameObject.transform.SetParent(rootAim.transform);
        gameObject.SetActive(false);
    }
}
