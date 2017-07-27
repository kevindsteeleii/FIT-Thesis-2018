using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public static bool grabbable = true;

	//allows for adjustment of enemy strength
	[Range (0,25)]
	public static int HP;

	private int fullHP;

	// Use this for initialization
	protected virtual void Start () {
		HP = 10;
		fullHP = HP;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
		if (grabbable) {canGrab ();		}

	}

	protected virtual void takeDamage(int dam){
		
		HP -= dam;
	}

	protected virtual bool canGrab () {
		bool canGrab;
		if (HP <= (fullHP / 2)) {
			this.gameObject.tag = "Grabbable";
			canGrab = true;
		} else
			canGrab = false;
		return canGrab;
	}

}
