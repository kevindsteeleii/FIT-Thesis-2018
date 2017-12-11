using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputTest : MonoBehaviour {

	// Use this for initialization
	private void Start () {
		InputObserver.onInputDown += (InputArgs args) => {
			if (args is TouchInputArgs) {
				TouchInputArgs touchArgs = (TouchInputArgs)args;

//				Debug.Log(args is MouseInputArgs);
//				Debug.Log(args is TouchInputArgs);
//				Debug.Log(args is KeyInputArgs);

				Debug.Log("Touch position on press down " + touchArgs.position.ToString() + ", at " + touchArgs.time.ToString("0.00") + " seconds.");
			}
		};
		InputObserver.onInput += (InputArgs args) => {
			if (args is TouchInputArgs) {
				TouchInputArgs touchArgs = (TouchInputArgs)args;

				Debug.Log("Touch position is " + touchArgs.position.ToString() + ", with a change in position of " + touchArgs.delta.ToString() + ", at " + touchArgs.time.ToString("0.00") + " seconds.");
			}
		};
		InputObserver.onInputUp += (InputArgs args) => {
			if (args is TouchInputArgs) {
				TouchInputArgs touchArgs = (TouchInputArgs)args;

				Debug.Log("Touch position on release " + touchArgs.position.ToString() + ", with a change in position of " + touchArgs.delta.ToString() + ", at " + touchArgs.time.ToString("0.00") + " seconds.");
			}
		};
	}
}
