using UnityEngine;

/// <summary>
/// Renders aiming reticle invisible upon release of button
/// </summary>
public class AimVisible : MonoBehaviour {

    public new Renderer renderer;
    // Use this for initialization
    void Awake () {
        renderer = this.GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!Input.GetButton("Aim"))
        {
            renderer.enabled = false;
        }
        else
        {
            renderer.enabled = true;
        }
    }
}
