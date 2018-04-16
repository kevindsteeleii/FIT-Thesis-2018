using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Test class for button mashing combos will implement in an event manager later
/// </summary>
public class TestCombos : MonoBehaviour
{

    [Tooltip("This is the timing range between button presses")]
    [Range(0f, 20f)]
    public float interval;

    //number of times button pressed in succession within interval's time frame
    int buttonPresses = 0;
    float lastPressed = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            lastPressed = Time.time;

            if (buttonPresses <= 0)
            {
                buttonPresses++;
                Debug.Log("Button pressed " + buttonPresses + " times!!");
            } 

            else
            {
                if (Input.GetKeyDown(KeyCode.Space) && Time.time <= (lastPressed + interval))
                {
                    buttonPresses++;
                    Debug.Log("Button pressed " + buttonPresses + " times!!");
                }
            }
        }

        if (Time.time > (lastPressed + interval))
        {
            buttonPresses = 0;
        }

    }

}
