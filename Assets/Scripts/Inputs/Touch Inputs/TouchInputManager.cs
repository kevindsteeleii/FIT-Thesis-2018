using System.Collections;
using UnityEngine;

public class TouchInputManager : Singleton<TouchInputManager> {
	public bool multiTouchEnabled = true;
	private Touch[] touches = new Touch[0];


	protected override void Awake () {
		base.Awake ();
		Input.multiTouchEnabled = this.multiTouchEnabled;
	}

	protected virtual void Update () {
		touches = Input.touches;

		for (int i = 0; i < touches.Length; i++) {
			if (touches[i].phase == TouchPhase.Began) InputObserver.OnInputDown (new TouchInputArgs (touches[i].fingerId, touches[i], touches[i].position, touches[i].deltaPosition));
			if (touches[i].phase == TouchPhase.Moved) InputObserver.OnInput (new TouchInputArgs (touches[i].fingerId, touches[i], touches[i].position, touches[i].deltaPosition));
			if (touches[i].phase == TouchPhase.Stationary) InputObserver.OnInput (new TouchInputArgs (touches[i].fingerId, touches[i], touches[i].position, touches[i].deltaPosition));
			if (touches[i].phase == TouchPhase.Ended) InputObserver.OnInputUp (new TouchInputArgs (touches[i].fingerId, touches[i], touches[i].position, touches[i].deltaPosition));
			if (touches[i].phase == TouchPhase.Canceled) InputObserver.OnInputUp (new TouchInputArgs (touches[i].fingerId, touches[i], touches[i].position, touches[i].deltaPosition));
		}
	}
}
