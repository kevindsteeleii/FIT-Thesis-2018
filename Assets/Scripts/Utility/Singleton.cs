using UnityEngine;

/// <summary>
/// Singleton behaviour class, used for components that should only have one instance
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
    private static T reference;
    public static T instance {
        get {
			return reference;
        }
    }

    /// <summary>
    /// Returns whether the instance has been initialized or not.
    /// </summary>
    public static bool IsInitialized {
        get {
			return reference != null;
        }
    }

    /// <summary>
    /// Base awake method that sets the singleton's unique instance.
    /// </summary>
    protected virtual void Awake () {
		if (reference != null) {
            Debug.LogErrorFormat("Trying to instantiate a second instance of singleton class {0}", GetType().Name);
            DestroyImmediate(gameObject);
        } else {
			reference = (T) this;
            DontDestroyOnLoad(gameObject);
        }
    }

    protected virtual void OnDestroy () {
		if (reference == this) {
			reference = null;
        }
    }
}
