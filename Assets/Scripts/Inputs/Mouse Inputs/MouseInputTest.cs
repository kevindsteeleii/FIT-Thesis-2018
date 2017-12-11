using UnityEngine;

public class MouseInputTest : MonoBehaviour {

	// Use this for initialization
	private void Start () {
		InputObserver.onInputDown += (InputArgs args) => {
			if (args is MouseInputArgs) {
				MouseInputArgs mouseArgs = (MouseInputArgs)args;

//				Debug.Log(args is MouseInputArgs);
//				Debug.Log(args is TouchInputArgs);
//				Debug.Log(args is KeyInputArgs);

				Debug.Log("Mouse position on press down " + mouseArgs.position.ToString() + ", at " + mouseArgs.time.ToString("0.00") + " seconds.");
			}
		};
		InputObserver.onInput += (InputArgs args) => {
			if (args is MouseInputArgs) {
				MouseInputArgs mouseArgs = (MouseInputArgs)args;

				Debug.Log("Mouse position is " + mouseArgs.position.ToString() + ", with a change in position of " + mouseArgs.delta.ToString() + ", at " + mouseArgs.time.ToString("0.00") + " seconds.");
			}
		};
		InputObserver.onInputUp += (InputArgs args) => {
			if (args is MouseInputArgs) {
				MouseInputArgs mouseArgs = (MouseInputArgs)args;

				Debug.Log("Mouse position on release " + mouseArgs.position.ToString() + ", with a change in position of " + mouseArgs.delta.ToString() + ", at " + mouseArgs.time.ToString("0.00") + " seconds.");
			}
		};
	}
}
