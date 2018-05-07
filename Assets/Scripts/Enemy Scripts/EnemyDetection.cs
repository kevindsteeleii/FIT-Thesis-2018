using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Real Enemy Patrol script
/// </summary>
public class EnemyDetection : MonoBehaviour {
    [Range(0.1f, 4)]
    public float speedMultiplier = 2;
    public GameObject post1, post2;
    public Animator myAnim;
    public bool stationary = false;
    float progress = 0.0f;
    int stopper = 1;    //used to modify the enemy by making it unable to move upon certain criteria
    private Quaternion currentRot;

    // Use this for initialization
    void Start () {

        if(myAnim != null)
        {
            return;
        }
        else
        {   //CAUTION**Animator must be the 1st child of the gameObject script is attached to for this to work!!!
            myAnim = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        }
		if (post1 != null)
        {
            return;
        }
        else
            post1 = gameObject.transform.root.GetChild(0).gameObject;   //comment out if the posts aren't in the same parent object as the platform itself

        if (post2 != null)
        {
            return;
        }
        else
            post2 = gameObject.transform.root.GetChild(1).gameObject;   //comment out if the posts aren't in the same parent object as the platform itself

        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StartCoroutine(StartPatrol());
    }

    private void FixedUpdate()
    {
        
        stopper = 1;
        myAnim.SetBool("stationary", stationary);
        if (myAnim.GetBool("enemyDetected") || myAnim.GetBool("meleeRange") || myAnim.GetBool("stationary"))
        {
            stopper = 0;
        }
        myAnim.SetInteger("stopper", stopper);
    }

    protected virtual void Turn()
    {
        gameObject.transform.rotation = currentRot;
    }

    IEnumerator StartPatrol ()
    {
        int progressMultiplier = 1;

        progress = 0f;

        while (true)
        {
            progress += Time.smoothDeltaTime *stopper * speedMultiplier * progressMultiplier;

            Debug.Log(string.Format("Progress equals {0}, and progressMultiplier equals {1}", progress, progressMultiplier));
            if (progressMultiplier > 0)
            {
                if (progress >= 1.0f)
                {
                    currentRot = new Quaternion(0, 0, 0, 1);
                    Turn();
                    progress = 1.0f;
                    progressMultiplier *= -1;
                }
            }
            else
            {
                if (progress <= 0.0f)
                {
                    currentRot = new Quaternion(0, 180, 0, 1);
                    Turn();
                    progress = 0.0f;
                    progressMultiplier *= -1;
                }
            }
            transform.position = Vector3.Lerp(post1.transform.position, post2.transform.position, progress);
            yield return null;
        }
    }

    /// <summary>
    /// Listens for an event that passes an int of 1 or 0
    /// to move/stop the enemy's movements
    /// </summary>
    /// <param name="stop"></param>
    public void On_Stopper_Received(int stop)
    {
        stopper = stop;
    }
}
