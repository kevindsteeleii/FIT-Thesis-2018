using UnityEngine;
/// <summary>
/// Handles transform and positioning data
/// </summary>
[RequireComponent(typeof (Projectile),typeof (ProjectileController ))]
public class ProjectileView : View {

    //used as reference point in place of 
    [SerializeField]
    private Projectile model;


	// Use this for initialization
	protected virtual void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected virtual void shooting()   {

    }
}
