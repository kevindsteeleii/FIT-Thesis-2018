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
        Debug.Log("The tag of collider entered is: " + other.gameObject.tag);
        if (other.gameObject.tag == "HurtBox" )
        {
            DestroyMe();
        }
        else if (other.gameObject.tag == "ActiveBox")
        {
            DestroyMe();
        }
    }

    //Doesn't "destroy" as much as set's active to false and rejoins it's old parent object in hierarchy
    public void DestroyMe()
    {
        gameObject.transform.SetParent(rootAim.transform);
        gameObject.SetActive(false);
    }
}
