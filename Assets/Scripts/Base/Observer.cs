using UnityEngine;
using System;
/// <summary>
/// Observer is the class that listens for a specific change in a Subject component
/// </summary>
public abstract class Observer: MonoBehaviour 
{

    public abstract void Update();

    /// <summary>
    /// Ideally this is the counter action to the notification from its Subject
    /// </summary>
    public abstract void OnNotify();
}
