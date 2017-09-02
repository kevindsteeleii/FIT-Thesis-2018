using UnityEngine;

/// <summary>
/// Class used to keep track of and populate a level manager class
/// </summary>
public class Platforms : MonoBehaviour {

    //transform info and collider to be used
    Transform location;
    Collider collider;

    /// <summary>
    /// enum used to establish the behavior state of the platform the default is normal
    /// </summary>
    public enum PlatformBehavior { Normal,Disappearing,Moving,Falling};
    PlatformBehavior behavior;

    // Use this for initialization
    private void Awake()
    {
        //sets default as standard
        behavior = PlatformBehavior.Normal;
        location = this.transform;
        collider = this.GetComponent<Collider>();
    }
    void Start() {
    }
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
