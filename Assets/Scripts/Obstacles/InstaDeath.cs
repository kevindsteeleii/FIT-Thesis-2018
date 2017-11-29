using UnityEngine;
using System;
using System.Collections;

/// <summary>
///Class attached to all objects that cause instant death state and subsequent game over. 
/// </summary>
public class InstaDeath : MonoBehaviour {

    [SerializeField]
    Collider deathBox;

    /// <summary>
    /// Event used to trigger instant death when player comes in contact with certain obstacles.
    /// </summary>
    public event Action InstantDeath;

    // Use this for initialization
    protected virtual void Start () {
        deathBox = this.gameObject.GetComponent<Collider>();
        this.gameObject.tag = "Death Object";
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
    }

}
