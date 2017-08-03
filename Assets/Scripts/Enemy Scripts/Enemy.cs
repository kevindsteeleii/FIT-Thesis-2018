using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public static bool grabbable;

	//allows for adjustment of enemy strength
	[Range (0,25)]
	public static int HP;

	private int fullHP;

	// Use this for initialization
	protected virtual void Awake () {
        HP = 10;
	}

    protected virtual void Update()
    {
        if (HP <= HP / 2)
            { }
        else
            { }
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

    private void OnCollisionEnter(Collision col)
    {
        
    }

    private void OnCollisionStay(Collision col)
    {
        
    }

    private void OnCollisionExit(Collision col)
    {
        
    }



}
