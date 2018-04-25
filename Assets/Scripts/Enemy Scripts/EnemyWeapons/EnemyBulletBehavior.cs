using System;
using System.Collections;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour {

    #region Global Variable Declaration

    [Tooltip("The time in seconds the bullets last")]
    [Range(.1f, 10f)]
    public float lifeTime = 0.5f;
    Rigidbody bulletRB;

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

    /// <summary>
    /// CoRoutine that times out the existence of the bullet so it doesn't go off forever
    /// </summary>
    /// <returns></returns>
    IEnumerator TimeOut()
    {
        if (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy();
        }
        yield return null;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy();
        //if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Ground")
        //{
        //    Destroy();
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ground")
        {
            Destroy();
        }
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