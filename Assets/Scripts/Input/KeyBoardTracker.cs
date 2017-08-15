using UnityEngine;

public class KeyBoardTracker : DeviceTracker
{

    public AxisButtons[] axisKeys;
    public KeyCode[] buttonKeys;

    /// <summary>
    /// On Right-click on Inspector on script autogenerates right number of pre-selected number of axis and buttons
    /// </summary>
    void Reset()    {
        im = GetComponent<InputManager>();
        axisKeys = new AxisButtons[im.axisCount];
        buttonKeys = new KeyCode[im.buttonCount];
    }
    // Update is called once per frame
    void Update()   {
        //checks for button presses and as long as they are identified as members of the array they are true
        for (int i = 0; i < buttonKeys.Length; i++) {
            if (Input.GetKey(buttonKeys[i]))    {
                data.buttons[i] = true;
                newData = true;
            }
        }

        for (int i = 0; i < axisKeys.Length; i++)   {
            if (Input.GetKey(axisKeys[i].positive)) {
                data.buttons[i] = true;
                newData = true;
            }
        }
        //check for inputs, if inputs detected, set newData to true and keeps it from perpetuating from frame to frame
        //populate InputData to pass to the InputManager

        if (newData)    {
            im.PassInput(data);
            //once passed set back to false 
            newData = false;
            data.Reset();
        }
    }
}


/// <summary>
/// handles the keyboard input of axes so that it will impossible to generate both positive and negative on same axis
/// </summary>
[System.Serializable]
public struct AxisButtons   {
    public KeyCode positive;
    public KeyCode negative;
}
