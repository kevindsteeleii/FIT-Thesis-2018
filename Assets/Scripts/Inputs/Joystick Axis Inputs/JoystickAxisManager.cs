using UnityEngine;

public class JoystickAxisManager : Singleton<JoystickAxisManager> {
    [SerializeField]
    protected float[] joystickAxis = new float[29];

    protected virtual void Update () {
        for (int i = 0; i < joystickAxis.Length; i++) {
            joystickAxis[i] = Input.GetAxis("Joy1Axis" + (i + 1));
            InputObserver.OnInput(new JoystickAxisInputArgs(i + 1, joystickAxis[i]));
        }
    }
}
