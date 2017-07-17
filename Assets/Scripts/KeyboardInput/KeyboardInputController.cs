using UnityEngine;

public class KeyboardInputController : MonoBehaviour {

    private void Awake() {
        KeyboardInputObserver.onKeyDown += (KeyboardInputParameters e) => {
            Debug.Log("The key " + e.keyCode.ToString() + " was pressed down " + e.time.ToString() + " seconds into the game.");
        };

        KeyboardInputObserver.onKeyUp += (KeyboardInputParameters e) => {
            Debug.Log("The key " + e.keyCode.ToString() + " was released " + e.time.ToString() + " seconds into the game.");
        };
    }
}
