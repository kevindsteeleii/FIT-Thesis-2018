using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenericDelegateTest : MonoBehaviour {

    public delegate void OnMessageReceived();
    public event Action OnMessageComplete;
    public event Action<int> InputChanged;

    public delegate void PassedParam<T> (T param);

    public delegate T ReturnParam<T, U> (U param);

    PassedParam <int> parmer = WhatsTheNumber;

    ReturnParam <string,char [] > newName = NameToPrint;

    public static void WhatsTheNumber (int no)
    {

    }

    public static string NameToPrint (char [] name)
    {
        String namePrint = "";

        foreach (char a in name)
        {
            namePrint += a;
        }

        return namePrint;
    }

    char[] example = {'a','b','c','d','e','f' };

    // Use this for initialization
    void Start () {
        parmer(5);
        newName(example);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
