using UnityEngine;

/// <summary>
/// Class used to create platforms and manage their behavior
/// </summary>
public class Platforms : MonoBehaviour {

    //transform info and collider to be used with the the platformbehavior enum
    Transform location;
    Collider collider;
    GameObject platform;

    /// <summary>
    /// enum used to establish the behavior state of the platform the 
    /// default is normal and upon 
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
        switch (behavior)
        {
            case PlatformBehavior.Disappearing:
                //choose an obstacle/platform behavior
                break;
            case PlatformBehavior.Normal:
                //does nothing, supports weight and platforming
                break;

            case PlatformBehavior.Moving:
                //choose an obstacle/platform behavior
                break;
            case PlatformBehavior.Falling:
                //choose an obstacle/platform behavior
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
