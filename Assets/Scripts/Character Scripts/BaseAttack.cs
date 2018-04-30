using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class for all types of attacks
/// </summary>
public abstract class BaseAttack : MonoBehaviour
{

    [Tooltip("The damage the this attack inflicts upon an enemy or destructible obstacle")]
    [Range(0, 20)]
    public int dam = 10;
    public SphereCollider myHitBox;
    public Transform attachedObject;
    public Vector3 offset;

    public event Action<int,string> On_TransferDamage_Sent;

    public virtual void FixedUpdate()   //handles the movement and adjustments needed as necessary to match up the colliders for attacks etc.
    {
        Vector3 changes = attachedObject.transform.position;
        changes.x = attachedObject.transform.position.x + offset.x;
        changes.y = attachedObject.transform.position.y + offset.y;
        changes.z = attachedObject.transform.position.z + offset.z;
        gameObject.transform.position = changes;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HurtBox" /*&& other.gameObject.layer == 12*/ && myHitBox.enabled)
        {
            Debug.Log("Hit the enemy hitbox");
            On_TransferDamage_Sent += other.gameObject.GetComponent<Enemy>().EnemyTakeDamage;
            On_TransferDamage_Sent(dam,gameObject.tag);
            On_TransferDamage_Sent -= other.gameObject.GetComponent<Enemy>().EnemyTakeDamage;
        }
    }
}
#region TODO list, refactoring etc
/************TODO Refactoring********************************************************************//*
 1-
 2-
 3-
 4-
 *************************************************************************************************/
#endregion