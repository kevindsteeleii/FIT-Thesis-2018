using UnityEngine;

/// <summary>
/// The GrabData Scriptable Object to be made into a saveable asset
/// </summary>
public class GrabData : ScriptableObject {
    //range of grab determined by slider
    [Tooltip("How far away he grabs")]
    [Range(0f, 10f)]
    public float grabRange;

    //speed variable inverse to actual speed
    [Tooltip("Smaller the number the faster it goes")]
    [Range(0f, 0.7f)]
    public float speed;

    //distance from origin, the default is pretty good right now
    public Vector2 offSet = new Vector2(0.6f, 0.6f);

}
