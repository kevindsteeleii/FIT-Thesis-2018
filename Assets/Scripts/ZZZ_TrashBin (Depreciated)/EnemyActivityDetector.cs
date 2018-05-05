using UnityEngine;

public class EnemyActivityDetector : MonoBehaviour {
    public GameObject body;
    public bool isActive = false;
    private void Start()
    {
        //gameObject.SetActive(false);
        //enabled = false;
        body = gameObject.transform.root.GetChild(2).gameObject;
        
    }

    private void Update()
    {
        body.GetComponent<EnemyDetection>().enabled= isActive;
        //gameObject.SetActive(enabled);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("The {0} object with tag {1} has been entered", other.gameObject, other.gameObject.tag));
        if (other.gameObject.tag == "MainCamera")
            isActive = true;
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log(string.Format("The {0} object with tag {1} has been stayed in", other.gameObject, other.gameObject.tag));
        if (other.gameObject.tag == "MainCamera")
            isActive = true;
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log(string.Format("The {0} object with tag {1} has been exited", other.gameObject, other.gameObject.tag));
        if (other.gameObject.tag == "MainCamera")
            isActive = false;
    }
}
#region TODO list, refactoring etc
/************TODO Refactoring********************************************************************//*
 1- 
 2-
 3-
 4-
 *************************************************************************************************/
#endregion