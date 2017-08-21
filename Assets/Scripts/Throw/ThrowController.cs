using UnityEngine;

/// <summary>
///	Takes input for ThrowModel
/// </summary>
public class ThrowController : Controller
{
    /*The following variables etc are for a test method 
     to prove projectile works */
    public GameObject testBullet;
    public GameObject rootAim;
    public GameObject aimReticle;
    [Range(400f, 3000f)]
    public float tempForce;

    protected virtual void FixedUpdate()
    {
        /*Uses Throw to trigger a branching nest of conditionals that will either throw a projectile
         at an angle or straight, or returns a debug log*/
        if (Input.GetButtonDown("Throw") && !Input.GetButton("Aim"))   {
            // Ammo.shootLoad();
            GameObject bullet;
            bullet = Instantiate(testBullet, rootAim.transform.position, rootAim.transform.rotation) as GameObject;
            Rigidbody tempRB;
            tempRB = bullet.GetComponent<Rigidbody>();
            tempRB.AddForce(transform.right * tempForce);
            Destroy(bullet, 10.0f);


        }

        else if (Input.GetButtonDown("Throw") && Input.GetButton("Aim"))        {
            GameObject bullet;
            bullet = Instantiate(testBullet, rootAim.transform.position, rootAim.transform.rotation) as GameObject;
            Rigidbody tempRB;
            tempRB = bullet.GetComponent<Rigidbody>();
            Vector3 direction = aimReticle.transform.position - rootAim.transform.position;
            tempRB.AddForceAtPosition(direction * tempForce, transform.position);
            Destroy(bullet, 10.0f);
        }
    }


}

