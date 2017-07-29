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


	protected virtual void Awake ()
	{
		if (!model)
			model = this.gameObject.GetComponent<ThrowModel> ();
		KeyboardInputObserver.onKeyDown += (KeyboardInputParameters obj) => {
			if (obj.keyCode == KeyCode.I) {
				//spawn object
				model.OnThrowStart();
			}
		};
		KeyboardInputObserver.onKeyUp += (KeyboardInputParameters obj) => {
			if (obj.keyCode == KeyCode.I) {
				//throw
				model.OnThrowEnd();

			}
		};

	}
}

