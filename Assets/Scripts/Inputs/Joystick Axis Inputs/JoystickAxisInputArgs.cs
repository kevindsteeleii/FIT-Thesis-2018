public class JoystickAxisInputArgs : InputArgs {
    public int axis = 0;
    public float value = 0.0f;

    public JoystickAxisInputArgs(int axis, float value) : base() {
        this.axis = axis;
        this.value = value;
    }
}
