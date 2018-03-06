using UnityEngine;

/// <summary>
/// Scriptable Object that holds the data for the PlayerStats Class
/// </summary>
[CreateAssetMenu (fileName ="Player Stats Data",menuName ="DataAsset/PlayerStats")]
public class StatsData : ScriptableObject {

    [Tooltip("Sets Player Health")]
    [Range(1, 100)]
    public int maxHP = 30;

    [Tooltip("Seconds of IFrames indicated by blinking")]
    [Range(2, 8)]
    public int duration = 6;

    [Tooltip("Wait Time describes the intervals between visible and invisible")]
    [Range(0, 1)]
    public float waitTime;

    [Tooltip(" Sets Upper limit of in-game currency or 'scrap' ")]
    [Range(0, 10000)]
    public int maxMoney;

    [Tooltip("")]
    [Range(0, 1000)]
    public int maxEnergy;

    [Tooltip("Percentage of money to take away upon death")]
    [Range(0, 30)]
    public int percentDeduction;



}
