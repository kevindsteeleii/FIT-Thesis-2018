using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The killabee turret firing script 
/// </summary>

public class Killabee_TurretFire : MonoBehaviour {

    //the empty game object used to launch bullets from
    public GameObject gunBarrel;
    public GameObject bullet;
    GameObject gunSight;
    PoolItem poolBullets;   //the pooled bullets
    int directionModifier = 1;
    public KillabeeAnimatorScript animEvent;
    //the number of shots that can be fired per second
    int fullClip = 0;

    //not in use atm, for the fire rate in seconds or 5 times a second .2 * 1.000 seconds
    [Range(0, 5)]
    public float fireRate = 1f;

    [Range(0, 100)]
    public float fireForce = 30f;

    [Tooltip("The percentage of shots per fire cycle that are grabbable")]
    [Range(0, 100)]
    public int percentage = 50;

    float percentFloat = .5f;

    // Use this for initialization
    protected virtual void Start()
    {
        
        percentFloat = percentage * 0.01f;  //converts percentage whole number to float form for weighted generation of bullets

        if (gunSight != null)
        {
            return;
        }
        else
        {
            gunSight = gunBarrel.transform.GetChild(0).gameObject;
        }

        fullClip = Mathf.RoundToInt(1 / fireRate);

        poolBullets = new PoolItem(fullClip, bullet);
        ChangeBullets();
        animEvent.On_Killabee_Shot += On_Firing_Received;
    }

    void On_Firing_Received()
    {
        if (!poolBullets.IsEmpty())
        {
            StartCoroutine(FireRoute());    //fires when player is detected
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine("StopFire");
        }
    }

    private void ChangeBullets()    //changes the kind of bullets the turret fires based on a percentage
    {
        int changedBullets = Mathf.FloorToInt(percentFloat * poolBullets.Count());
        List<int> previousIndexes = new List<int>();
        int randomIndex;

        for (int i = changedBullets; i >= 0; i--)
        {
            do
            {
                randomIndex = UnityEngine.Random.Range(0, poolBullets.Count());
            }
            while (previousIndexes.Contains(randomIndex));

            poolBullets.GetAtIndex(randomIndex).GetComponent<EnemyBulletTest>().SetGrabbable(true);
            previousIndexes.Add(randomIndex);
        }
    }

    private void Update()
    {
        percentFloat = percentage * 0.01f;  //converts percentage whole number to float form for weighted generation of bullets

        if (poolBullets.Count() <= 0)
        {
            StartCoroutine(WaitToReload());
            poolBullets.ReUse();
        }
    }

    void FireFix() //using pooled objects to fire projectiles
    {
        if (fullClip >0)
        {
            try
            {
                GameObject temp = poolBullets.Get(gunBarrel.transform.position);
                temp.GetComponent<Rigidbody>().velocity = Vector3.right * directionModifier * fireForce;
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("Out of bullets, enemy");
                throw;
            }
            fullClip--;
        }
    }

    IEnumerator FireRoute()
    {
        for (int i = 0; i < poolBullets.Count(); i++)
        {
            FireFix();
            yield return new WaitForSecondsRealtime(1f);
        }

        yield return null;
    }

    IEnumerator WaitToReload()
    {
        yield return new WaitForSecondsRealtime(.75f);
    }

    IEnumerator StopFire()
    {
        for (int i = 0; i < poolBullets.Count(); i++)
        {
            poolBullets.ReUse();
            fullClip++;
        }
        yield return null;
    }

    private void FixedUpdate()
    {
        directionModifier = (gunSight.transform.position.x - gunBarrel.transform.position.x < 0) ? -1 : 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("{0} is the tag of the object {1} that was collided with", other.tag, other.gameObject));
        //Debug.Log(other.gameObject + " collider detected");
        if (other.tag == "Player")
        {
            if (!poolBullets.IsEmpty())
            {
                StartCoroutine(FireRoute());    //fires when player is detected
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine("StopFire");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(StopFire()); //reloads when not detected
        }
    }
}
#region TODO list, refactoring etc
/****************************************TODO************************************************
1-
2-
3-
4-
5-
6-
****************************************TODO************************************************/
#endregion
