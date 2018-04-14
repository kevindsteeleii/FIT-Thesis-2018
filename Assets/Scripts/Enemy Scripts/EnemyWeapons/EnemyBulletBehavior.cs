using System;
using System.Collections;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour {

    #region Global Variable Declaration

    [Tooltip("The time in seconds the bullets last")]
    [Range(.1f, 10f)]
    public float lifeTime = 0.5f;

    [Tooltip("The maximum distance the projectile can be shot from until it becomes inactive and is recollected in the pool manager")]
    [Range(.1f, 30f)]
    public float fireDistance = 15f;

    Vector3 initDistance;

    Rigidbody bulletRB;

    float xDistance;    //used to measure relative distance
    #endregion
    
	// Use this for initialization
	void Start () {
		if (bulletRB != null)
        {
            return;
        }
        else
        {
            bulletRB = gameObject.GetComponent<Rigidbody>();
        }

        StartCoroutine(TimeOut());
	}
	
	// Update is called once per frame
	void Update () {

        xDistance = Mathf.Abs(gameObject.transform.position.x - initDistance.x);

        if( xDistance >= fireDistance)
        {
            Destroy();
        }
	}

    /// <summary>
    /// CoRoutine that times out the existence of the bullet so it doesn't go off forever
    /// </summary>
    /// <returns></returns>
    IEnumerator TimeOut()
    {
        if (gameObject.activeInHierarchy)
        {
            yield return new WaitForSecondsRealtime(lifeTime);
            Destroy();
        }
        yield return null;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        initDistance = gameObject.transform.position;
    }

    private void OnDisable()
    {
        xDistance = 0;
    }
}
#region TODO list, refactoring etc
/****************************************TODO************************************************
  1-Make bullet grabbale but only if grabbable
  2-Make it so that bullets have a timeout/distance out
  3**-add particle effect and sound cue for grab conversion of grabbable bulllets
  4-have a "destruction" on colllision functionality added to the bullet
  5-
  6-
****************************************TODO************************************************/
#endregion