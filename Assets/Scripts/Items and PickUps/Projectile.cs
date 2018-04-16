using UnityEngine;
using System;
using System.Collections;
/// <summary>
/// Class that houses logic for Projectile
/// </summary>
public class Projectile : Model
{ 
    GameObject projectileObj;
    SphereCollider mySphere;
    Vector3 initPos;

    //damage the projectile causes
    [Tooltip ("The timeout in seconds for the projectile life cycle")]
    [Range (0f,15f)]
    public float outTime = 3f;

    [Tooltip("The zoneout in seconds for the projectile life cycle")]
    [Range(0f, 30f)]
    public float outOfBounds = 15f;

    [Tooltip("The amount of damage done by the projectile")]
    [Range(0, 15)]
    public int damage = 10;

    /// <summary>
    /// event that transmsits the physical game object body of the projectile
    /// </summary>
    public event Action<GameObject> On_BulletDestroyed_Sent;
   
    public virtual void Start()
    {
        if (projectileObj == null)
        {
            projectileObj = this.gameObject;
        }

        if(mySphere != null)
        {
            return;
        }
        else
        {
            mySphere = gameObject.GetComponent<SphereCollider>();
        }
        On_BulletDestroyed_Sent += Ammo.instance.Reuse;
    }


    //IEnumerator ProjectileTimeOut()
    //{
    //    if (gameObject.activeInHierarchy)
    //    {
    //        yield return new WaitForSeconds(outTime);
    //        gameObject.SetActive(false);
    //    }
    //    yield return null;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HurtBox" || other.gameObject.tag == "DeathBox")
        {
            On_BulletDestroyed_Sent(projectileObj);
        }
        if (other.gameObject.tag == "VisionCone")
        {
            Physics.IgnoreCollision(mySphere, other, true);
        }
    }

    //private void Update()
    //{
    //    if (gameObject.activeInHierarchy)
    //    {
    //        Debug.Log("Position is " + initPos);
    //    }

    //    float distance = Vector3.Distance(initPos, gameObject.transform.position);
    //    if (Mathf.Abs(distance) >= outOfBounds)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}

    private void OnEnable()
    {
        //StartCoroutine(ProjectileTimeOut());
        initPos = gameObject.transform.position;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}