using System.Collections;
using UnityEngine;

public class MouseInputManager : Singleton<MouseInputManager> {
	protected readonly Vector2 hidden = new Vector2 (-1000, -1000);

	protected Vector2 mouseButtonPosition;

	protected virtual void Update () {
		if (Input.GetMouseButtonDown (0)) {
			InputObserver.OnInputDown (new MouseInputArgs (Input.mousePosition));
			mouseButtonPosition = Input.mousePosition;
		}
		if (Input.GetMouseButton (0)) {
			Vector2 delta = (Vector2)Input.mousePosition - mouseButtonPosition;
			InputObserver.OnInput (new MouseInputArgs (Input.mousePosition, delta));
			mouseButtonPosition = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp (0)) {
			Vector2 delta = (Vector2)Input.mousePosition - mouseButtonPosition;
			InputObserver.OnInputUp (new MouseInputArgs (Input.mousePosition, delta));
		}
	}
}