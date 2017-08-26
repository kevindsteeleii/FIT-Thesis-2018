using UnityEngine;

public class Enemy : MonoBehaviour
{

    public bool grabbable;

    //the physical body of the enemy itself
    [SerializeField]
    public static GameObject body;

    //allows for adjustment of enemy health points
    [Range(0, 25)]
    public int HP;
    int saveHP;

    [Range(0, 25)]
    public int damage;

    // Use this for initialization
    void Start()
    {
        body = this.gameObject;
        //grabbable = false;
        saveHP = HP;
        grabbable = false;
    }

    void Update()
    {
        //checks the current HP vs. fullHP and if current is <= half of full HP change
        if (HP <= saveHP / 2 && HP > 0)
        {
            grabbable = true;
        }
        else if (HP > saveHP / 2)
        {
            grabbable = false;
        }
        else if (HP <= 0)
        {
            HP = 0;
            Destroy(body);
        }
    }

    public void takeDamage(int dam)
    {
        HP -= dam;
    }

    /// <summary>
    /// Changes the tag of the enemy and transforming the body into a projectile.
    /// </summary>
    public void becomeProjectile()
    {
        Ammo.load();
        Destroy(body);

    }

    void OnCollisionEnter(Collision col)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            takeDamage(other.gameObject.GetComponent<Projectile>().damage);
            Debug.Log("Hit");
            Destroy(other.GetComponent<GameObject>());
        }

        else if (other.gameObject.tag == "Hand" && grabbable)
        {
            becomeProjectile();
            Debug.Log("Grabbed");
        }

        else if (other.gameObject.tag == "Hand" && !grabbable)
        {
            takeDamage(other.gameObject.GetComponent<GrabModel>().damage);
            Debug.Log("Grope");
        }

    }

}
