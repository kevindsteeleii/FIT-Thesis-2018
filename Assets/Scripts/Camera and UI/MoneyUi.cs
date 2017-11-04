using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUi : MonoBehaviour {
	public Text MoneyText;
	//dosh=common but possibly outdated English Slang term for Money
	//int dosh;
	// Use this for initialization
	void Start () {
		MoneyText.text = "Oh hai Money";
        //subscribes the GetMoney method's subscription from 
        PlayerStats.instance.MoneyAmount += GetMoney;
    }
	
	// Update is called once per frame
	//void Update () {
	//	dosh = PlayerStats.instance.wallet;
	//	MoneyText.text = ""+dosh;
	//}

    /// <summary>
    /// GetMoney is a method that updates upon changes in the subject of its subscription
    /// the PlayerStats class' wallet
    /// </summary>
    /// <param name="money"></param>
    public void GetMoney (int money)
    {
        MoneyText.text = "" + money;
    }
}
