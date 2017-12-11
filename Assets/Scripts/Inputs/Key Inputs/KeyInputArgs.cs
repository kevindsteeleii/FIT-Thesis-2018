using UnityEngine;

public class KeyInputArgs : InputArgs {
	public KeyCode keyCode;

	public KeyInputArgs (KeyCode keyCode) : base () {
		this.keyCode = keyCode;
	}
}