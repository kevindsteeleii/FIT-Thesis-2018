using UnityEngine;

public class MouseInputArgs : InputArgs {
	public Vector2 position = new Vector2(0.0f, 0.0f);
	public Vector2 delta = new Vector2(0.0f, 0.0f);

	public MouseInputArgs (Vector2 position) : base () {
		this.position = position;
	}

	public MouseInputArgs (Vector2 position, Vector2 delta) : base () {
		this.position = position;
		this.delta = delta;
	}
}
