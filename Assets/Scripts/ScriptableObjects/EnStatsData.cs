using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy Stats Data", menuName = "DataAsset/Enemy Stats Data")]
public class EnStatsData :ScriptableObject{

    [Header("Attack 1 Damage")]
    [Range(1, 15)]
    public int dmg = 5;

    [Header("Attack 2 Damage")]
    [Range(1, 15)]
    public int dmg2 = 5;

    [Header("Closest Distance to Edge")]
    [Range(0.2f, 2f)]
    public float minDistance = 0.4f;

    [Header("Movement Speed")]
    [Range(0f, 10f)]
    public float speed = 0.5f;

    [Header("Attack Range")]
    [Range(0f, 2f)]
    public float range = 0.4f;

    [Header("Detection Range")]
    [Range(0.1f, 10f)]
    public float detectPlayerRange = 2f;

    [Header("Fire Rate")]
    [Range(1, 6)]
    public int fireRate = 1;

    [Header("Force of Projectile")]
    [Range(0.1f, 100f)]
    public float shotForce;

    [Header("Attack Interval")]
    [Range(0f, 4f)]
    public float whiffPunishWindow = 1.5f;

    [Header("Ammo clip")]
    [Range(3, 12)]
    public int bulletAmount;

    /// <summary>
    /// float that returns the time scaled attact interval in seconds
    /// </summary>
    public float AttackInterval
    {
        get
        {
            return whiffPunishWindow * Time.fixedDeltaTime;
        }
        set
        {
            value = whiffPunishWindow * Time.fixedDeltaTime;
        }
    }
}
