using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingData : ScriptableObject {

    [Tooltip("Intensity of throw")]
    [Range(400f, 1000f)]
    public float throwForce;

    [Tooltip("Decreases speed of aimed throw")]
    [Range(0f, 0.9f)]
    public float aimThrowSpeed;

    [Tooltip("Determines height offset of the beginning of throw ")]
    [Range(0.1f, 4f)]
    public float throwHeightOffset;

}
