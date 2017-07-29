﻿using UnityEngine;

/// <summary>
/// The controller class for the AnimProxy.
/// This class checks for inputs.
/// </summary>
public class AimProxyController : Controller
{
    /// <summary>
    /// A reference to the model class.
    /// </summary>
    [SerializeField]
    private AimProxyModel model;

    protected virtual void Awake()
    {
        // check to see if the model variable is empty
        if (!model)
        {
            // if it is then get the model attached to the current GameObject
            model = this.gameObject.GetComponent<AimProxyModel>();
        }
    }

    protected virtual void Update()
    {
        // Check if the aim button is being pressed down
        if (Input.GetButtonDown("Aim"))
        {
            // if it is then start the progress update loop in our model
            model.StartProgressUpdateLoop();
        }
        // Check if the aim button is being released
        if (Input.GetButtonUp("Aim"))
        {
            // if it is then end the progress update loop in our model
            model.EndProgressUpdateLoop();
        }

    }
}