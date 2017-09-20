using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletControl : MonoBehaviour {
	public Sprite[] Shots;
    public Image BulletImage;
	float test;
	
	void Update(){
		BulletImage.sprite = Shots[Ammo.instance.bullets];
	}
}
