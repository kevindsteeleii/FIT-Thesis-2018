using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstracted class used to pass along it's data to its inheritors 
/// i.e. Keyboard or Game Pad Trackers
/// </summary>
[RequireComponent (typeof(InputManager))]
public abstract class DeviceTracker : MonoBehaviour {

    //needs keyboard tracker to be able to track this
    protected InputManager im;
    protected InputData data;


    protected bool newData;

    private void Awake()    {
        im = GetComponent<InputManager>();
        data = new InputData(im.axisCount, im.buttonCount);
    }

}
