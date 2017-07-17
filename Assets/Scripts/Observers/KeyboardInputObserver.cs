using System;
using UnityEngine;

public class KeyboardInputObserver {
    /// <summary>
    /// event that fires off when a key on the keyboard is pressed down.
    /// </summary>
    public static event Action<KeyboardInputParameters> onKeyDown;
    /// <summary>
    /// event that fires off when a key on the keyboard is released.
    /// </summary>
    public static event Action<KeyboardInputParameters> onKeyUp;

    /// <summary>
    /// trigger the event for a key being pressed down.
    /// </summary>
    /// <param name="parameters">information about what key is being pressed.</param>
    public static void OnKeyDown (KeyboardInputParameters parameters) {
        // check to make sure something is subscribed to the event
        if (onKeyDown != null) {
            // if there is then fire those methods off
            onKeyDown(parameters);
        }
    }

    /// <summary>
    /// trigger the event for a key being released.
    /// </summary>
    /// <param name="parameters">information about what key is being released.</param>
    public static void OnKeyUp (KeyboardInputParameters parameters) {
        // check to make sure something is subscribed to the event
        if (onKeyUp != null) {
            // if there is then fire those methods off
            onKeyUp(parameters);
        }
    }
}


public class KeyboardInputParameters {
    /// <summary>
    /// the key that was pressed
    /// </summary>
    public KeyCode keyCode;
    /// <summary>
    /// when in unscaled game time it was pressed
    /// </summary>
    public float time;

    public KeyboardInputParameters (KeyCode keyCode) {
        this.keyCode = keyCode;
        this.time = Time.unscaledTime;
    }
}