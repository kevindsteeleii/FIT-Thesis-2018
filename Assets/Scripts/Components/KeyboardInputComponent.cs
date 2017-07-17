using UnityEngine;

public class KeyboardInputComponent : MonoBehaviour {
    public KeyCode keyCode = KeyCode.A;

    private void Start () {
        KeyboardInputManager.instance.Subscribe(this.keyCode);
    }
}
