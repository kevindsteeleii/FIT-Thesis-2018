using UnityEngine;

public class Enemy : MonoBehaviour {

	public bool grabbable;

    //the physical body of the enemy itself
    [SerializeField]
    public static GameObject body;

	//allows for adjustment of enemy health points
	[Range (0,25)]
	public int HP;
    int saveHP;

	// Use this for initialization
	void Start () {
        body = this.gameObject;
        //grabbable = false;
        saveHP = HP;
        grabbable = false;
	}

    void Update()    {
        //checks the current HP vs. fullHP and if current is <= half of full HP change
        if (HP <= saveHP / 2)   {
            this.gameObject.tag = "Projectile";
            grabbable = true;
        }
        else  {
            grabbable = false;
        }
    }

	public void takeDamage(int dam)  {		
		HP -= dam;
	}

    /// <summary>
    /// Changes the tag of the enemy and transforming the body into a projectile.
    /// </summary>
    public void becomeProjectile()    {
        Ammo.load();
        Destroy(body);

    }
    
    void OnCollisionEnter(Collision col)    {
        //Debug.Log("Collision");

        //if (col.gameObject.tag == "Hand" && grabbable)
        //{
        //    becomeProjectile();
        //}

        //else if (col.gameObject.tag == "Hand" && !grabbable)
        //{
        //    takeDamage(col.gameObject.GetComponent<GrabModel>().damage);
        //}
    }

}
