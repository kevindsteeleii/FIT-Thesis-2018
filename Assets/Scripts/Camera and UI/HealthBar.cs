using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Singleton<HealthBar>
{

    /*instantiates the mainSlider, which is our healthbar, the data
     asset that saves information of parameters as asset data, the canvas group for
     the visual changes to be made to the fill rect*/

    Slider mainSlider;

    [SerializeField]
    StatsData data;

    /*The Canvas group to be made invisible upon death and visible upon respawn etc*/
    [SerializeField]
    CanvasGroup sliderGroup;
    //Animator myAnim;

    // Use this for initialization
    void Awake()
    {
        /*sets the minimum and maximum values for the health slider*/
        mainSlider = this.GetComponent<Slider>();
        mainSlider.minValue = 0;
        mainSlider.maxValue = data.maxHP;
        sliderGroup.alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //updating HP value for the slider
        //Debug.Log("Player HP is " + PlayerStats.instance.hp);
        mainSlider.value = PlayerStats.instance.hp;

        if (PlayerStats.instance.hp <= 0)
        {
            sliderGroup.alpha = 0;
        }
        else if (PlayerStats.instance.hp > 0)

        {
            sliderGroup.alpha = 1;
        }
    }
}
