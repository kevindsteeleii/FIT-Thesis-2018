using UnityEngine;

/// <summary>
/// A view class for our AnimProxy which requires a Transform
/// component on the game object it is on in order to work.
/// </summary>
[RequireComponent(typeof(Transform), typeof(AimProxyModel), typeof(AimProxyController))]
public class AimProxyView : View {
    /// <summary>
    /// A reference to the model class.
    /// </summary>
    [SerializeField]
    private AimProxyModel model;
    /// <summary>
    /// The minimum relative position.
    /// </summary>
    [SerializeField]
    protected Vector3 startRelativePosition = new Vector3(0, 0, 0);
    /// <summary>
    /// The maximum relative position.
    /// </summary>
    [SerializeField]
    protected Vector3 endRelativePosition = new Vector3(0, 0, 0);


    protected virtual void Awake() {
        // check to see if the model variable is empty
        if (!model) {
            // if it is then get the model attached to the current GameObject
            model = this.gameObject.GetComponent<AimProxyModel>();
        }
        // subscribe to even that updates
        model.onProgressUpdated += ChangePosition;
    }

    /// <summary>
    /// The callback method for the onProgressUpdate event of the model.
    /// </summary>
    /// <param name="progress">The current progress of the animation</param>
    protected virtual void ChangePosition(float progress) {
        // Update the position based on a spherical-linear interpolation of the
        // startRelativePosition, and the endRelativePosition based on where the progress is.
        this.transform.localPosition = Vector3.Slerp(startRelativePosition, endRelativePosition, progress);
    }
}
