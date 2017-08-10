using UnityEngine;

/// <summary>
/// Renders aiming reticle invisible upon release of button
/// </summary>
public class AimVisible : MonoBehaviour {

    public static Vector3 reticlePos;


    public new Renderer renderer;
    // Use this for initialization
    void Awake () {
        renderer = this.GetComponent<Renderer>();
        reticlePos = this.gameObject.transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        reticlePos = this.gameObject.transform.position;
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
