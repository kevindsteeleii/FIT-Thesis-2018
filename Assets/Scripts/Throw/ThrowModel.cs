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
    [Range(400f, 3000f)]
    public float throwForce;
    static float throwForceProxy;

    [Tooltip("Decreases speed of aimed throw")]
    [Range(0f, 0.9f)]
    public float aimThrowSpeed;
    static float aimThrowSpeedProxy;

    [Tooltip("Determines height offset of the beginning of throw ")]
    [Range(0.1f, 4f)]
    public float throwHeightOffset;
    static float throwOffset;

    static Vector3 myPos,right,direction,up;

    static GameObject testBulletProxy, rootAimProxy,aimReticleProxy;
    

    // Use this for initialization
    void Awake () {
        testBulletProxy = testBullet;
        
        throwForceProxy = throwForce;
        aimThrowSpeedProxy = aimThrowSpeed;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        myPos = transform.position;
        right = transform.right;
        throwOffset = throwHeightOffset;
        up = transform.up;
        aimReticleProxy = aimReticle;
        rootAimProxy = rootAim;
       direction = aimReticleProxy.transform.position - rootAimProxy.transform.position;
    }

    public static void throwStraight()
    {
        GameObject bullet;
        bullet = Instantiate(testBulletProxy, rootAimProxy.transform.position, rootAimProxy.transform.rotation) as GameObject;
        Rigidbody tempRB;
        tempRB = bullet.GetComponent<Rigidbody>();
        tempRB.AddForce(right * throwForceProxy);
        Ammo.shootLoad();
        Destroy(bullet, 10.0f);
    }

    public static void throwAngle()
    {
        GameObject bullet;
        bullet = Instantiate(testBulletProxy, rootAimProxy.transform.position, rootAimProxy.transform.rotation) as GameObject;
        Rigidbody tempRB;
        tempRB = bullet.GetComponent<Rigidbody>();        
        tempRB.AddForceAtPosition(direction * throwForceProxy *aimThrowSpeedProxy, myPos);
        Ammo.shootLoad();
        Destroy(bullet, 10.0f);
    }

    //unplugged vertical throw , yet to be assigned inputs
    public static void throwUp()
    {
        GameObject bullet;
        bullet = Instantiate(testBulletProxy, rootAimProxy.transform.position, rootAimProxy.transform.rotation) as GameObject;
        Rigidbody tempRB;
        tempRB = bullet.GetComponent<Rigidbody>();
        tempRB.AddForce(up * throwForceProxy);
        Vector3 pos = myPos;
        pos.y += throwOffset;
        tempRB.AddForceAtPosition(up * throwForceProxy, pos, ForceMode.VelocityChange);
        Ammo.shootLoad();
        Destroy(bullet, 10.0f);
    }
}
