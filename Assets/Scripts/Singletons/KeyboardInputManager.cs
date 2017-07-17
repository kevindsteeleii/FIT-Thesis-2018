using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : Singleton<KeyboardInputManager> {
    protected List<KeyCode> keys = new List<KeyCode>();

    private void Update () {
        for (int i = 0; i < this.keys.Count; i++) {
            if (Input.GetKeyDown(this.keys[i])) KeyboardInputObserver.OnKeyDown(new KeyboardInputParameters(this.keys[i]));
            if (Input.GetKeyUp(this.keys[i])) KeyboardInputObserver.OnKeyUp(new KeyboardInputParameters(this.keys[i]));
        }
    }

    /// <summary>
    /// Subscribe a key to be listened for.
    /// </summary>
    /// <param name="keyCode">The keycode you want to listen for.</param>
    public void Subscribe(KeyCode keyCode) {
        // check to make sure we aren't already listening for this key.
        if (!this.keys.Contains(keyCode)) {
            // if not then add it to the list.
            this.keys.Add(keyCode);
        }
    }

    /// <summary>
    /// Unsubscribe a key from being listened for.
    /// </summary>
    /// <param name="keyCode">The keycode you want to stop listening for.</param>
    public void Unsubscribe(KeyCode keyCode) {
        // check to make sure we are listening for this key.
        if (this.keys.Contains(keyCode)) {
            // if so then remove it from the list.
            this.keys.Remove(keyCode);
        }
    }
}
