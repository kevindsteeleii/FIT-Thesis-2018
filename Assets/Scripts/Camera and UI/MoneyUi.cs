using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUi : MonoBehaviour {
	public Text MoneyText;
	//dosh=common but possibly outdated English Slang term for Money
	int dosh;
	// Use this for initialization
	void Start () {
		MoneyText.text = "Oh hai Money";
	}
	
	// Update is called once per frame
	void Update () {
		dosh = PlayerStats.wallet;
		MoneyText.text = ""+dosh;
	}
}
