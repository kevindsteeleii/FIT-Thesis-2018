using UnityEngine;

public class Enemy : MonoBehaviour {

	public static bool grabbable;

    //the physical body of the enemy itself
    [SerializeField]
    public static GameObject body;

	//allows for adjustment of enemy health points
	[Range (0,25)]
	public int HP;
    int saveHP;

	// Use this for initialization
	protected virtual void Awake () {
        body = this.gameObject;
        grabbable = false;
        saveHP = HP;
	}

    protected virtual void Update()    {
        //checks the current HP vs. fullHP and if current is <= half of full HP change
        if (HP <= saveHP / 2)   {
            this.gameObject.tag = "Projectile";
            grabbable = true;
        }
        else  {
            grabbable = false;
        }
        Debug.Log(HP);
    }

	public void takeDamage(int dam)  {		
		HP -= dam;
	}

    /// <summary>
    /// Changes the tag of the enemy and transforming the body into a projectile.
    /// </summary>
    protected virtual void becomeProjectile()    {
        Destroy(body);
        Ammo.load();
    }

    private void OnCollisionEnter(Collision col)    {

        if (col.gameObject.CompareTag("Hand") && grabbable)   {
            becomeProjectile();           
        }

        else if (col.gameObject.CompareTag("Hand") && !grabbable)    {            
            takeDamage(col.gameObject.GetComponent<GrabModel>().damage);
        }

        else if (col.gameObject.CompareTag("Projectile")) {            
            takeDamage(col.gameObject.GetComponent<Projectile>().damage);
        }           
    } 
}
