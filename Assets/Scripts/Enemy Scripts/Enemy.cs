using UnityEngine;

public class Enemy : MonoBehaviour {

    public bool grabNow;
	public static bool grabbable;

    //the physical body of the enemy itself
    [SerializeField]
    public static GameObject body;

	//allows for adjustment of enemy strength
	[Range (0,25)]
	public static int HP;

	private int fullHP;

	// Use this for initialization
	protected virtual void Awake () {
        body = this.gameObject;
        grabbable = false;
        fullHP = HP;
        HP = 10;
        this.gameObject.tag = "Enemy";
        grabbable = grabNow;
	}

    protected virtual void Update()    {
        //checks the current HP vs. fullHP and if current is <= half of full HP change
        if (HP <= fullHP / 2)   {
            this.gameObject.tag = "Projectile";
            grabbable = true;
        }
        else  {
            grabbable = false;
        }
        if (grabbable)
        {

        }
    }

	protected virtual void takeDamage(int dam)  {		
		HP -= dam;
	}

    /// <summary>
    /// Changes the tag of the enemy and transforming the body into a projectile.
    /// </summary>
    protected static void becomeProjectile()    {
        Destroy(body);
    }

    private void OnCollisionEnter(Collision col)    {

            if (col.gameObject.tag == "Hand" && grabbable)   {
                becomeProjectile();
            }
           
    }

    private void OnCollisionStay(Collision col)    {
        
    }

    private void OnCollisionExit(Collision col)    {
        
    }
}
