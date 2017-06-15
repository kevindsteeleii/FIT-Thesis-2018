using System;
using UnityEngine;

/// <summary>
/// The model stores all the data that gets edited
/// </summary>
public class PreloaderModel : Model {
    /// <summary>
    /// This is an event that gets fired when the progress is changed.
    /// </summary>
	public event Action<float> onProgressUpdate;
    /// <summary>
    /// This is an event that gets fired when the progress is at 100%
    /// </summary>
	public event Action onProgressComplete;

    /// <summary>
    /// The progress of loading the next scene.
    /// </summary>
	public float progress {
		get {
			return _progress;
		}
		set {
			_progress = 0;

			if (onProgressUpdate != null)
				onProgressUpdate (value);

			if (value == 1.0 && onProgressComplete != null)
				onProgressComplete ();
		}
	}

    /// <summary>
    /// Private reference for the preloader progress.
    /// </summary>
	private float _progress = 0.0f;
}
