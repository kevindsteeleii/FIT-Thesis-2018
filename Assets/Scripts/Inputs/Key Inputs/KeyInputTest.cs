using UnityEngine;

public class KeyInputTest : MonoBehaviour {
	
	// Use this for initialization
	private void Start () {
		InputObserver.onInputDown += (InputArgs args) => {
			if (args is KeyInputArgs) {
				KeyInputArgs keyArgs = (KeyInputArgs)args;

//				Debug.Log(args is MouseInputArgs);
//				Debug.Log(args is TouchInputArgs);
//				Debug.Log(args is KeyInputArgs);

				Debug.Log("The " + keyArgs.keyCode.ToString() + " key is pressed down at " + keyArgs.time.ToString("0.00") + " seconds.");
			}
		};
		InputObserver.onInput += (InputArgs args) => {
			if (args is KeyInputArgs) {
				KeyInputArgs keyArgs = (KeyInputArgs)args;

				Debug.Log("The " + keyArgs.keyCode.ToString() + " key is held down at " + keyArgs.time.ToString("0.00") + " seconds.");
			}
		};
		InputObserver.onInputUp += (InputArgs args) => {
			if (args is KeyInputArgs) {
				KeyInputArgs keyArgs = (KeyInputArgs)args;

				Debug.Log("The " + keyArgs.keyCode.ToString() + " key is released at " + keyArgs.time.ToString("0.00") + " seconds.");
			}
		};
	}
}
