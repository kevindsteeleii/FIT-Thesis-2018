using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : Controller {

	[SerializeField]
	private Projectile projectile;

	[SerializeField]
	private List <Projectile> ammo;
	protected virtual void Awake () {
		//check to see if projectile is there
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
