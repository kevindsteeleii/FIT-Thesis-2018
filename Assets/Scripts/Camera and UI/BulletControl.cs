using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletControl : MonoBehaviour {
	public Sprite[] Shots;
    public Image BulletImage;
	
	void Update(){
       // Debug.Log("Bullets available "+ Ammo.bullets);
		BulletImage.sprite = Shots[Ammo.bullets];
	}
}
