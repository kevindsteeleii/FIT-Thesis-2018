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
    void Start()
    {
        /*sets the minimum and maximum values for the health slider*/
        mainSlider = this.GetComponent<Slider>();
        mainSlider.minValue = 0;
        mainSlider.maxValue = data.maxHP;
        sliderGroup.alpha = 1;

        //GetHPBar listens for the event of HPAmount changing
        PlayerStats.instance.On_HPAmount_Sent += GetHPBar;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //updating HP value for the slider
    //    //Debug.Log("Player HP is " + PlayerStats.instance.hp);
    //    NewMethod();
    //}

    /// <summary>
    /// HP is updated by the event HPAmount from PlayerStats
    /// </summary>
    /// <param name="hp"></param>
    void GetHPBar(int hp)
    {
        mainSlider.value = hp;

        if (hp <= 0)
        {
            sliderGroup.alpha = 0;
        }
        else if (hp > 0)

        {
            sliderGroup.alpha = 1;
        }
    }
}
