using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Changes visual/transpositional states of ThrowModel
/// </summary>
public class ThrowView : View {
	[SerializeField]
	protected ThrowModel model;


	protected virtual void Awake () {
		if (!model)
			model = this.gameObject.GetComponent<ThrowModel> ();
		model.onThrowStart += () => {
			//handle visual stuff that happens when you press down the throw button
			//projectile spawned on press
		};
		model.onThrowEnd += (float arg1, ForceMode arg2) => {
			//Throw item at this point_ arg1 is the intensity force passed and arg2 is the type of force passed
		};
	}


}
