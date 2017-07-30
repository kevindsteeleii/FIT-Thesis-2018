using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage and process input of any kind
/// </summary>
public class InputManager : MonoBehaviour {

    [Range (0,6)]
    public int axisCount, buttonCount;

    //takes input data and passes it along to 
    public void PassInput (InputData data)    {

    }

}

/// <summary>
/// struct used to qualify the number and kind of inputs and adjust flow of input accordingly
/// </summary>
public struct InputData
{
    public float[] axes;
    public bool[] buttons;

    public InputData (int axisCount, int buttonCount)
    {
        axes = new float[axisCount];
        buttons = new bool[buttonCount];
    }

    /// <summary>
    /// Resets axes inputs to neutral and buttons to false
    /// </summary>
    public void Reset()
    {
        for (int i = 0; i < axes.Length; i++)
        {
            axes[i] = 0;
            for (int j = 0; j < buttons.Length; j++)
            {
                buttons[i] = false;
            }

        }
    }
}
