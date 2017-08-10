using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///	Takes input for ThrowModel
/// </summary>
public class ThrowController : Controller
{

	[SerializeField]
	protected ThrowModel model;

    [SerializeField]
    private Ammo ammo;

    Rigidbody myRb;

    protected virtual void Awake ()
	{
        myRb = this.GetComponent<Rigidbody>();
  //      if (!model)
		//	model = this.gameObject.GetComponent<ThrowModel> ();
		//KeyboardInputObserver.onKeyDown += (KeyboardInputParameters obj) => {
		//	if (obj.keyCode == KeyCode.I) {
		//		//spawn object
		//		model.OnThrowStart();
		//	}
		//};
		//KeyboardInputObserver.onKeyUp += (KeyboardInputParameters obj) => {
		//	if (obj.keyCode == KeyCode.I) {
		//		//throw
		//		model.OnThrowEnd();

		//	}
		//};

	}

    protected virtual void Update()
    {
        //Mind you the "Grab" button doubles as 
        if (Input.GetButton("Grab") && !Ammo.emptyClip)
        {
            Ammo.shootLoad();
        }
    }

    protected void throwAngle()    {

    }
}

