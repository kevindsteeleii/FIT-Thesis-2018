using System;
using UnityEngine;

public class InputObserver {
	public static event Action<InputArgs> onInputDown;
	public static event Action<InputArgs> onInput;
	public static event Action<InputArgs> onInputUp;

	public static void OnInputDown (InputArgs args) {
		if (onInputDown != null) {
			onInputDown (args);
		}
	}

	public static void OnInput (InputArgs args) {
		if (onInput != null) {
			onInput (args);
		}
	}

	public static void OnInputUp (InputArgs args) {
		if (onInputUp != null) {
			onInputUp (args);
		}
	}
}
