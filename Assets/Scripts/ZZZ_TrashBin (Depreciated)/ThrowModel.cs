using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Handles logic of throw
/// </summary>
public class ThrowModel : Model {
 
	public float force;
	public ForceMode forceMode;

	public event Action onThrowStart;
	public event Action<float,ForceMode> onThrowEnd;

	public virtual void OnThrowStart(){
		if (onThrowStart != null) {
			onThrowStart();
		}
	}

	public virtual void OnThrowEnd(){
		if (onThrowEnd != null) {
			onThrowEnd(force, forceMode);

		}
	}

}
