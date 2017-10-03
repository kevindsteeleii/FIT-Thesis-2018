using UnityEngine;
/// <summary>
/// Used to manage projectiles ammo count
/// any reference made to Ammo singleton follow this example
/// ClassName.instance.publicVariable
/// or ClassName.instance.publicFunction()
/// </summary>
public class Ammo : Singleton<Ammo>
{
    //number of bullets


    [Tooltip("Choose between 0 and 6 'Bullets' to preload Ammo class")]
    [Range(0, 6)]
    public int testLoad;
    public int capacity = 6;
    int cap;

    /*To make parameter accessible outside of Singleton set up as a get and set parameter like so*/
    public int bullets { get; set; }

    // Use this for initialization
    void Start()
    {
        bullets = testLoad;
        cap = capacity;
    }


    public void load()
    {
        bullets++;
        Mathf.Clamp(bullets, 0, cap);

        if (bullets >= cap)
        {
            bullets = cap;
            Debug.Log("Filled to max capacity! Try throwing one.");
        }
        else
            Debug.Log("Loading...Now you have " + bullets + " bullets!!");
    }

    /// <summary>
    /// Deletes from bullet int and launches different throwing
    /// </summary>
    public void shootLoad()
    {
        bullets--;
        Mathf.Clamp(bullets, 0, cap);

        if (Input.GetButton("Throw") && bullets <= 0)
        {
            bullets = 0;
            Debug.Log("No Ammo, Empty Clip");
        }

        else if (Input.GetButton("Throw") && bullets >= 0)
        {

            Debug.Log("Shots fired! Only " + bullets + " shots left!");
        }
        else
            return;
    }
}
