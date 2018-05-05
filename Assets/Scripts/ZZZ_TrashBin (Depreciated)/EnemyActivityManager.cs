using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Does what it sounds like. Enemies within a certain range of the "activity bubble" or active otherwise they are not
/// </summary>
public class EnemyActivityManager : MonoBehaviour {
    [SerializeField]
    Transform PlayerCharacter;
    Vector3 location;   //variable fed the location of the player character
    public Vector3 ManagerLocation;
    BoxCollider myCollider;
	// Use this for initialization
	void Start () {
		if (PlayerCharacter != null)
        {
            return;
        }
        else
        {
            PlayerCharacter = GameObject.FindGameObjectWithTag("Player").transform/*.root*/;
        }

        if (myCollider != null)
        {
            return;
        }
        else
        {
            myCollider = gameObject.GetComponent<BoxCollider>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        location = PlayerCharacter.transform.position;
        transform.position = new Vector3(location.x,transform.position.y,location.z);
        
        ManagerLocation = transform.position;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyActiveBox")
            Debug.Log(string.Format("The {0} object with tag {1} has been entered", other.gameObject, other.gameObject.tag));
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(string.Format("The {0} object with tag {1} has stayed", other.gameObject, other.gameObject.tag));
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(string.Format("The {0} object with tag {1} has been exited", other.gameObject, other.gameObject.tag));
    }
}
