using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The controller is what fires off commands to your model
/// </summary>
public class PreloaderController : Controller {
	[SerializeField]
	private PreloaderModel model;

    /// <summary>
    /// The async operation of loading in the next scene.
    /// </summary>
	private AsyncOperation operation;

	// Use this for initialization
	private IEnumerator Start () {
        // wait for one second for visual reasons
		yield return new WaitForSeconds (1.0f);

        // set the operator to loading the second scene in your chain
        // the first scene should be your preloader
		operation = SceneManager.LoadSceneAsync (1);
        // do not allow the scene to automatically start
		operation.allowSceneActivation = false;

        // check to make sure the next scene isn't done loading
		while (!operation.isDone) {
            // update the progress based on the load progres of your next scene
			model.progress = operation.progress;
            // check to see if the scene is done loading
            // 0.9 is as far as the next scene loads before
            // actually switching to the next scene
			if (operation.progress == 0.9f) {
                // update the progress in your model to 1
                // so that your loader bar can animate a done state
				model.progress = 1.0f;
                // then wait for one more second
                // again, for visual reasons
				yield return new WaitForSeconds (1.0f);
                // then allow the scene to switch to the preloaded scene
				operation.allowSceneActivation = true;
			}
			yield return null;
		}
	}
}
