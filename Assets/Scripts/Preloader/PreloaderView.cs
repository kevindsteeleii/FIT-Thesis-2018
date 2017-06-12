using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The view is all visual commands
/// This shouldn't store any game logic
/// </summary>
public class PreloaderView : View {
	[SerializeField]
	private PreloaderModel model;
    /// <summary>
    /// The progress bar visual
    /// </summary>
	[Space]
	public Image progressBar;
    /// <summary>
    /// The text that tells you the current progress
    /// </summary>
	public Text progressText;

    /// <summary>
    /// Method called at the very moment this object becomes created
    /// </summary>
	private void Awake () {
		model.onProgressUpdate += OnProgressUpdate;
		model.onProgressComplete += OnProgressComplete;
	}

    /// <summary>
    /// Method called just before this object gets destroyed
    /// </summary>
	private void OnDestroy () {
		model.onProgressUpdate -= OnProgressUpdate;
		model.onProgressComplete -= OnProgressComplete;
	}

    /// <summary>
    /// Callback method for when the level preload progress is updated
    /// </summary>
    /// <param name="progress">Level preload progress</param>
	private void OnProgressUpdate (float progress) {
        // the string representation of the level preload progress
		string progressPercent = (progress * 100).ToString ("###");
        // update progress text with the current progress percentage value
		progressText.text = "Progress: " + progressPercent + "%";
        // extend the progress bar based on how done loading the next level
		progressBar.fillAmount = progress;
	}

    /// <summary>
    /// Empty callback method for when progress is completed
    /// </summary>
	private void OnProgressComplete () {}
}
