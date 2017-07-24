using UnityEngine;

[RequireComponent(typeof(Transform))]
public class AimProxyView : MonoBehaviour {
    [SerializeField]
    private AimProxyModel model;
    [SerializeField]
    private Vector3 startRelativePosition = new Vector3(0, 0, 0);
    [SerializeField]
    private Vector3 endRelativePosition = new Vector3(0, 0, 0);

    private void Awake() {
        // subscribe to even that updates
        model.onProgressUpdated += ChangePosition;
    }

    private void ChangePosition (float progress) {
        this.transform.localPosition = Vector3.Lerp(startRelativePosition, endRelativePosition, progress);
    }
}
