using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; //add buttonsu

public class TestGUI : StandaloneInputModule {

    private static StandaloneInputModule Instance;
    public static StandaloneInputModule instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = new TestGUI();
            }
            return Instance;
        }
    }    

	// Use this for initialization
	protected override void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        float move = 0;
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            move = Input.GetAxis("Horizontal");
            Debug.Log("Horizontal value is: " + move);
        }
    }

}
