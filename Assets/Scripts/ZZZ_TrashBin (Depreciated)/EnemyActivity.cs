using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivity : MonoBehaviour {
    #region Global Variable Declaration
    [SerializeField]
    GameObject mainBody;    //literally the main body of the enemy and the third child of the root
    #endregion
    // Use this for initialization
    void Start () {
		if (mainBody != null)
        {
            return;
        }
        else
        {
            mainBody = gameObject.transform.GetChild(2).gameObject;
        }
        mainBody.SetActive(false);
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("Entering Collider of {0} object has tag, {1}", other.gameObject, other.gameObject.tag));
        GameObject obj = other.gameObject;
        if (obj.tag == "ActiveBox")
        {
            mainBody.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(string.Format("Staying in Collider of {0} object has tag, {1}", other.gameObject, other.gameObject.tag));
        GameObject obj = other.gameObject;
        if (obj.tag == "ActiveBox")
        {
            mainBody.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(string.Format("Exiting Collider of {0} object has tag, {1}", other.gameObject, other.gameObject.tag));
        GameObject obj = other.gameObject;
        if (obj.tag == "ActiveBox")
        {
            mainBody.SetActive(false);
        }
    }
}
