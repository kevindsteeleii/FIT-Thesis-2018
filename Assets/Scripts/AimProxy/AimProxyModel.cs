using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// The class which houses all the logic for our AimProxy.
/// </summary>
public class AimProxyModel : Model {

    // A Vector 3 that tells the position of the aiming reticle in real time
    GameObject proxy;

    protected virtual void Start() {
        proxy = this.gameObject;
    }

    /// <summary>
    ///returns the current physical position of the aiming reticle
    /// </summary>
    public  Vector3 currentPosition() {
        Vector3 proxyPosition;
        proxyPosition = proxy.transform.position;
        return proxyPosition;


}

    public virtual void FixedUpdate()
    {

    }

    /// <summary>
    /// An event which triggers when the animation progress for the AnimProxy is updated.
    /// </summary>
    public event Action<float> onProgressUpdated;
    /// <summary>
    /// An event which triggers only when the animation for the AnimProxy starts.
    /// </summary>
    public event Action onProgressUpdateStart;
    /// <summary>
    /// An event which triggers only when the animation for the AnimProxy ends.
    /// </summary>
    public event Action onProgressUpdateEnd;

    /// <summary>
    /// A multiplier used to speed up or slow down the animation.
    /// </summary>
    [Tooltip("A multiplier used to speed up or slow down the animation.")]
    public float multiplier = 1.0f;

    /// <summary>
    /// A variable to house the progress of our animation.
    /// It is serialized so that we can view it in the editor even though it is a protected variable.
    /// </summary>
    [SerializeField]
    protected float progress = 0.0f;
    /// <summary>
    /// A variable to house the coroutine for updating the progress loop.
    /// </summary>
    protected Coroutine updateProgressLoop;

    /// <summary>
    /// A method to begin updating the animation progress.
    /// Will always start the animation at 0 progress and will ping-pong the animation until told to stop.
    /// </summary>
    public virtual void StartProgressUpdateLoop () {
        // end current animation if we have one
        EndProgressUpdateLoop();

        // check to see if anything is subscribed to onProgressUpdateStart
        if (onProgressUpdateStart != null) {
            // if there is then trigger the event
            onProgressUpdateStart();
        }

        // start a new animation loop from 0 progress.
        updateProgressLoop = StartCoroutine("UpdateProgressLoop");
    }

    /// <summary>
    /// End the current progress update loop
    /// </summary>
    public virtual void EndProgressUpdateLoop () {
        // check to see if we have a progress upodate loop running
        if(updateProgressLoop != null) {
            // if we do then end it.
            StopCoroutine(updateProgressLoop);
            // and set the stored progress upodate loop to null.
            updateProgressLoop = null;

            // check to see if anything is subscribed to onProgressUpdateEnd
            if (onProgressUpdateEnd != null) {
                // if there is then trigger the event
                onProgressUpdateEnd();
            }
        }
    }


    /// <summary>
    /// An IEnumerator that holds the animation logic for
    /// </summary>
    protected virtual IEnumerator UpdateProgressLoop () {
        // reset progress to 0
        progress = 0.0f;
        // create a progress multiplier to change between positive and negative addition
        int progressMultiplier = 1;
        // make a while loop that never ends
        while (true) {
            // check to see if the progress update event has a subscriber
            if (onProgressUpdated != null) {
                // if it does then broadcast the event
                onProgressUpdated(progress);
            }

            // wait until the frame ends
            yield return new WaitForEndOfFrame();
            // increase the progress by a mixture of smoothedDeltaTime (for better animation)
            // the base multiplier to speed up or slow down the animation
            // and the progress multiplier to add or subtract
            progress += Time.smoothDeltaTime * multiplier * progressMultiplier;
            // check if the progress multiplier is positive
            if (progressMultiplier > 0) {
                // if it is then check if the progress is greater than or equal to 1
                if (progress >= 1.0f) {
                    // if it is then sret the progress to exactly 1
                    progress = 1.0f;
                    // and make the progress multiplier negative
                    progressMultiplier *= -1;
                }
            } else {
                // if not then check if the progress is equal to or less than 0
                if (progress <= 0.0f) {
                    // if it is then set the progress to exactly 0
                    progress = 0.0f;
                    // and make the multiplier positive again
                    progressMultiplier *= -1;
                }
            }

            // return nothing so that we can loop
            yield return null;
        }
    }
}
