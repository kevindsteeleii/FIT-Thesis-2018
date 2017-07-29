using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Changes visual, transpositional, and states of ThrowModel
/// </summary>
public class ThrowView : View {
	[SerializeField]
	protected ThrowModel model;

	protected virtual void Awake () {
		if (!model)
			model = this.gameObject.GetComponent<ThrowModel> ();
	}
}
