using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that houses logic for Projectile
/// </summary>
public class Projectile : Model {
	//Class used to create projectile with its own velocity


	[SerializeField]
	private Projectile projectile;

	[SerializeField]
	public static int shots;

	public static List <GameObject> ammo = new List <GameObject>();

	//damage the projectile causes
	public static float damage;

	[SerializeField]
	protected AimProxyModel reticle;

	protected virtual void Awake () {
	}

	// Use this for initialization
	void Start () {
		shots = ammo.Count;
	}

	// Update is called once per frame
	protected virtual void Update () {
		
	}
	//check for projectile 
	public virtual bool checkProjectile() {
		bool hasAmmo;
		hasAmmo = (ammo == null) ? false : true;

		return hasAmmo;
	}

	public virtual void ammoLoader (){		
		GameObject [] myAmmo = GameObject.FindGameObjectsWithTag ("Projectile");
		ammo.AddRange (myAmmo);
	}

	public virtual void shootLoad () {

	}
}
