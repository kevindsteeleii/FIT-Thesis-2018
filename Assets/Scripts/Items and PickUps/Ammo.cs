using UnityEngine;

/// <summary>
/// Used to manage projectiles ammo count
/// </summary>
public class Ammo : MonoBehaviour
{
    //number of bullets

    public static int bullets;
    [Tooltip("Choose between 0 and 6 'Bullets' to preload Ammo class")]
    [Range(0, 6)]
    public int testLoad;
    public int capacity = 6;
    static int cap;

    // Use this for initialization
    void Awake()
    {
        bullets = testLoad;
        cap = capacity;
    }

    public static void load()
    {
        bullets++;

        if (bullets >= cap)
        {
            bullets = cap;
            Debug.Log("Filled to max capacity! Try throwing one.");
        }
    }

    /// <summary>
    /// Deletes from bullet int and launches different throwing
    /// </summary>
	public static void shootLoad()
    {
        if (Input.GetButton("Throw") && bullets <= 0)
            Debug.Log("No Ammo, Empty Clip");

        else if (Input.GetButton("Throw") && bullets >= 0)
        {
            bullets--;
            Debug.Log("Shots fired! Only " + bullets + " shots left!");
        }
        else
            return;
    }
}
