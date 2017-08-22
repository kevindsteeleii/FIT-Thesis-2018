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
    protected static void becomeProjectile()    {
        Destroy(body);
    }

    private void OnCollisionEnter(Collision col)    {

        if (col.gameObject.CompareTag("Hand") && grabbable)   {
            becomeProjectile();
            Ammo.load();            
        }

        else if (col.gameObject.CompareTag("Hand") && !grabbable)    {
            GrabModel hand = col.gameObject.GetComponent<GrabModel>();
            takeDamage(hand.damage);
        }

        else if (col.gameObject.CompareTag("Projectile")) {
            Projectile bullet = col.gameObject.GetComponent<Projectile>();
            takeDamage(bullet.damage);
        }           
    } 
}
