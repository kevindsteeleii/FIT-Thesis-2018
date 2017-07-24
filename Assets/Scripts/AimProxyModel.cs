using System;
using System.Collections;
using UnityEngine;

public class AimProxyModel : MonoBehaviour {
    public event Action<float> onProgressUpdated;
    private float progress = 0.0f;
    private Coroutine updateProgressLoop;

    public void StartProgressUpdateLoop () {
        EndProgressUpdateLoop();
        updateProgressLoop = StartCoroutine("UpdateProgressLoop");
    }

    public void EndProgressUpdateLoop () {
        if(updateProgressLoop != null) {
            StopCoroutine(updateProgressLoop);
            updateProgressLoop = null;
        }
    }

    private IEnumerator UpdateProgressLoop () {
        progress = 0.0f;
        int multiplier = 1;
        while (true) {
            yield return new WaitForEndOfFrame();
            progress += Time.smoothDeltaTime * multiplier;
            if (multiplier > 0) {
                if (progress >= 1.0f) {
                    progress = 1.0f;
                    multiplier *= -1;
                }
            } else {
                if (progress <= 0.0f) {
                    progress = 0.0f;
                    multiplier *= -1;
                }
            }
            if (onProgressUpdated != null) {
                onProgressUpdated(progress);
            }

            yield return null;
        }
    }
}
