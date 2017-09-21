using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAdds : Singleton<GrabAdds>
{
    //creates list 
    private List<GameObject> adds = new List<GameObject>();

    [Tooltip("The number of alloted adds to be added after the initial hand's launch!!")]
    [Range(3,8)]
    public int addNumber = 5;

	// Use this for initialization
	void Start () {
        adds.Capacity = addNumber;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
