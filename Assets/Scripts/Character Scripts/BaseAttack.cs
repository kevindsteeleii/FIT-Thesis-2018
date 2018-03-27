using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class for all types of attacks
/// </summary>
public abstract class BaseAttack : MonoBehaviour {

    [Tooltip("The damage the this attack inflicts upon an enemy or destructible obstacle")]
    [Range(0, 20)]
    public int dam = 10;

    public event Action<int> On_TransferDamage_Sent;

    //deals with the attack logic specific to the type of collider used
    public abstract void AttackDetection();

    /// <summary>
    /// Function that handles the logic of enemies taking damage as an
    /// Observer pattern
    /// </summary>
    /// <param name="cols"></param>
    protected virtual void EnemyHit(Collider [] cols)
    {
        foreach (Collider col in cols)
        {
            if (col.tag == "HurtBox")
            {
                On_TransferDamage_Sent += col.gameObject.GetComponentInParent<Enemy>().EnemyTakeDamage;
                On_TransferDamage_Sent(dam);
                On_TransferDamage_Sent -= col.gameObject.GetComponentInParent<Enemy>().EnemyTakeDamage;
            }
        }
    }
}
