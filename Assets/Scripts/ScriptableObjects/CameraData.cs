using UnityEngine;
[CreateAssetMenu(fileName ="Camera Data",menuName ="DataAsset/Camera Data")]
public class CameraData : ScriptableObject {
    [Tooltip("Camera Target")]
    [SerializeField]
    Transform Player;

    [Tooltip("The Vector 3 offset from the target")]
    [SerializeField]
    Vector3 Offset;

    [Tooltip("The smoothing speed of camera movement as a percentage")]
    [Range(0f, 1f)]
    public float smoothSpeed = 0.125f;    
}
