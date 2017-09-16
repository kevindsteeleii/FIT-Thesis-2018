using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Singleton/One-off for the health bar of the character in game
/// </summary>
public class HealthBar : MonoBehaviour {

    /*instantiates the mainSlider, which is our healthbar*/
    [SerializeField]
    Slider mainSlider;

    [SerializeField]
    StatsData data;


    // Use this for initialization
    void Awake () {
        /*sets the minimum and maximum values for the health slider*/
        mainSlider.minValue = 1;
        mainSlider.maxValue = data.maxHP;
    }
	
	// Update is called once per frame
	void Update () {
        //updating HP value for the slider

        mainSlider.value = PlayerStats.hp;
        if (PlayerStats.hp <= 0)
        {
            OnDeath();
        }
        else if (mainSlider.enabled==false && PlayerStats.hp > 0)
        {
            //mainSlider.enabled = true;
        }
    }
 
    /*Deals with the player death*/
    private void OnDeath()
    {
        mainSlider.value = 0;
        mainSlider.enabled = false;
    }
}
