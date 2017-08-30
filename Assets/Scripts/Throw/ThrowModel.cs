using UnityEngine;

/// <summary>
/// Class used as empty model that generates the instantiated bullet to be shot
/// </summary>
public class ThrowModel : Model {
    [Tooltip("The projectile prefab")]
    public GameObject testBullet;

    [Tooltip("The empty gameObject used as source or origin of aim")]
    public GameObject rootAim;

    [Tooltip("The aim reticle")]
    public GameObject aimReticle;

    [Tooltip("Intensity of throw")]
    [Range(400f, 1000f)]
    public float throwForce;

    [Tooltip("Decreases speed of aimed throw")]
    [Range(0f, 0.9f)]
    public float aimThrowSpeed;

    [Tooltip("Determines height offset of the beginning of throw ")]
    [Range(0.1f, 4f)]
    public float throwHeightOffset;

    Vector3 right,direction,up;
    
    // Use this for initialization
    void Awake () {
        right = transform.right;
        up = transform.up;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
       direction = aimReticle.transform.position - rootAim.transform.position;
    }

    public void throwStraight()
    {
        GameObject bullet;
        bullet = Instantiate(testBullet, rootAim.transform.position, rootAim.transform.rotation) as GameObject;
        Rigidbody tempRB;
        tempRB = bullet.GetComponent<Rigidbody>();
        tempRB.AddForce(right * throwForce);
        Ammo.shootLoad();
        Destroy(bullet, 10.0f);
    }

    public void throwAngle()
    {
        GameObject bullet;
        bullet = Instantiate(testBullet, rootAim.transform.position, rootAim.transform.rotation) as GameObject;
        Rigidbody tempRB;
        tempRB = bullet.GetComponent<Rigidbody>();        
        tempRB.AddForceAtPosition(direction * throwForce * aimThrowSpeed, transform.position);
        Ammo.shootLoad();
        Destroy(bullet, 10.0f);
    }

    /// <summary>
    /// Vertical throw with a plugged in offset expressed as a float
    /// </summary>
    public void throwUp()
    {
        GameObject bullet;
        bullet = Instantiate(testBullet, rootAim.transform.position, rootAim.transform.rotation) as GameObject;
        Rigidbody tempRB;
        tempRB = bullet.GetComponent<Rigidbody>();
        tempRB.AddForce(up * throwForce);
        Vector3 pos = transform.position;
        pos.y += throwHeightOffset;
        tempRB.AddForceAtPosition(up * throwForce, pos, ForceMode.VelocityChange);
        Ammo.shootLoad();
        Destroy(bullet, 10.0f);
    }
}
