using System.Collections;
using UnityEngine;

public class KeyInputManager : Singleton<KeyInputManager> {
	public KeyCode[] keyCodes;

	protected virtual void Update () {
		for (int i = 0; i < keyCodes.Length; i++) {
			if (Input.GetKeyDown(keyCodes[i])) InputObserver.OnInputDown (new KeyInputArgs(keyCodes[i]));
			if (Input.GetKey(keyCodes[i])) InputObserver.OnInput (new KeyInputArgs(keyCodes[i]));
			if (Input.GetKeyUp(keyCodes[i])) InputObserver.OnInputUp (new KeyInputArgs(keyCodes[i]));
		}
	}
}
