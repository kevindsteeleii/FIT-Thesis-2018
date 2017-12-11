using UnityEngine;

public class TouchInputArgs : InputArgs {
	public int finger = 0;
	public Touch touch;
	public Vector2 position = new Vector2(0.0f, 0.0f);
	public Vector2 delta = new Vector2(0.0f, 0.0f);

	public TouchInputArgs (int finger, Vector2 position) : base () {
		this.finger = finger;
		this.position = position;
	}

	public TouchInputArgs (int finger, Touch touch, Vector2 position, Vector2 delta) : base () {
		this.finger = finger;
		this.touch = touch;
		this.position = position;
		this.delta = delta;
	}
}
