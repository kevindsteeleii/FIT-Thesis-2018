using System;
using UnityEngine;

namespace ThesisDebuggingTools {
	/// <summary>
	/// Substitutes controller inputs for keyboard inputs.
	/// </summary>
	public class ProxyController : MonoBehaviour {
		public static event Action<KeyPressed> onControllerKeyDown, onControllerKeyUp;

		public KeyCode leftShoulder, rightShoulder;
		public KeyCode a, b, x, y;
		public KeyCode up, down, left, right;
		public KeyCode _start, _select;

		private void Update () {
			// face a key pressed
			if (Input.GetKeyDown(a)) {
				if (onControllerKeyDown != null) {
					onControllerKeyDown (KeyPressed.a);
				}
			}
			if (Input.GetKeyUp(a)) {
				if (onControllerKeyUp != null) {
					onControllerKeyUp (KeyPressed.a);
				}
			}

			// face b key pressed
			if (Input.GetKeyDown(b)) {
				if (onControllerKeyDown != null) {
					onControllerKeyDown (KeyPressed.b);
				}
			}
			if (Input.GetKeyUp(b)) {
				if (onControllerKeyUp != null) {
					onControllerKeyUp (KeyPressed.b);
				}
			}

			// face x key pressed
			if (Input.GetKeyDown(x)) {
				if (onControllerKeyDown != null) {
					onControllerKeyDown (KeyPressed.x);
				}
			}
			if (Input.GetKeyUp(x)) {
				if (onControllerKeyUp != null) {
					onControllerKeyUp (KeyPressed.x);
				}
			}

			// face y key pressed
			if (Input.GetKeyDown(y)) {
				if (onControllerKeyDown != null) {
					onControllerKeyDown (KeyPressed.y);
				}
			}
			if (Input.GetKeyUp(y)) {
				if (onControllerKeyUp != null) {
					onControllerKeyUp (KeyPressed.y);
				}
			}

			// up key pressed
			if (Input.GetKeyDown(up)) {
				if (onControllerKeyDown != null) {
					onControllerKeyDown (KeyPressed.up);
				}
			}
			if (Input.GetKeyUp(up)) {
				if (onControllerKeyUp != null) {
					onControllerKeyUp (KeyPressed.up);
				}
			}

			// down key pressed
			if (Input.GetKeyDown(down)) {
				if (onControllerKeyDown != null) {
					onControllerKeyDown (KeyPressed.down);
				}
			}
			if (Input.GetKeyUp(down)) {
				if (onControllerKeyUp != null) {
					onControllerKeyUp (KeyPressed.down);
				}
			}

			// left key pressed
			if (Input.GetKeyDown(left)) {
				if (onControllerKeyDown != null) {
					onControllerKeyDown (KeyPressed.left);
				}
			}
			if (Input.GetKeyUp(left)) {
				if (onControllerKeyUp != null) {
					onControllerKeyUp (KeyPressed.left);
				}
			}

			// right key pressed
			if (Input.GetKeyDown(right)) {
				if (onControllerKeyDown != null) {
					onControllerKeyDown (KeyPressed.right);
				}
			}
			if (Input.GetKeyUp(right)) {
				if (onControllerKeyUp != null) {
					onControllerKeyUp (KeyPressed.right);
				}
			}

			// left shoulder key pressed
			if (Input.GetKeyDown(leftShoulder)) {
				if (onControllerKeyDown != null) {
					onControllerKeyDown (KeyPressed.leftShoulder);
				}
			}
			if (Input.GetKeyUp(leftShoulder)) {
				if (onControllerKeyUp != null) {
					onControllerKeyUp (KeyPressed.leftShoulder);
				}
			}

			// right shoulder key pressed
			if (Input.GetKeyDown(rightShoulder)) {
				if (onControllerKeyDown != null) {
					onControllerKeyDown (KeyPressed.rightShoulder);
				}
			}
			if (Input.GetKeyUp(rightShoulder)) {
				if (onControllerKeyUp != null) {
					onControllerKeyUp (KeyPressed.rightShoulder);
				}
			}

			// start key pressed
			if (Input.GetKeyDown(_start)) {
				if (onControllerKeyDown != null) {
					onControllerKeyDown (KeyPressed.start);
				}
			}
			if (Input.GetKeyUp(_start)) {
				if (onControllerKeyUp != null) {
					onControllerKeyUp (KeyPressed.start);
				}
			}

			// select key pressed
			if (Input.GetKeyDown(_select)) {
				if (onControllerKeyDown != null) {
					onControllerKeyDown (KeyPressed.select);
				}
			}
			if (Input.GetKeyUp(_select)) {
				if (onControllerKeyUp != null) {
					onControllerKeyUp (KeyPressed.select);
				}
			}
		}
	}

	public enum KeyPressed : int {
		up = 0,
		down = 1,
		left = 2,
		right = 3,
		a  = 4,
		b  =5, 
		x = 6,
		y = 7,
		leftShoulder = 8,
		rightShoulder=9,
		start = 10,
		select = 11
	}
}
