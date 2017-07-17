using UnityEngine;

public class KeyboardInputComponent : MonoBehaviour {
    public KeyCode keyCode = KeyCode.A;

    private void Awake () {
        KeyboardInputManager.instance.Subscribe(this.keyCode);
    }
}
