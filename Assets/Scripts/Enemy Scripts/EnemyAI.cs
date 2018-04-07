using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBehavior { Stationary, Patrolling, Pursuing, Attacking, Shooting, Turret, Floating, Dive_Bombing};
/// <summary>
/// abstract class from which enemy types derive their behavior and inner logic
/// </summary>
public abstract class EnemyAI : MonoBehaviour
{
    public EnemyBehavior enemyBehavior;

    Enemy thisEnemy;
    public GameObject bullet;   //this is the projectile used by the enemy
    public Rigidbody myRb;

    #region Delete Section Soon
    //[Tooltip("Time between attack turns")]
    //[Range(0.3f, 3f)]
    //public float _attackInterval;

    //time between attacks
    //float attackInterval;

    //[Tooltip("Time between attack turns")]
    //[Range(0f, 6f)]
    //public float_movementSpeed;

    //speed of movement
    //float movementSpeed;

    //[Tooltip("The closest enemy can be to edge before turning")]
    //[Range(0f, .5f)]
    //public float _minTurnDistance;

    //the buffer distance b/n enemy patrolling and platform edge
    //float minTurnDistance;

    //[Tooltip("Number of shots fired per interval")]
    //[Range(1, 6)]
    //public int _shotsPerInterval;

    //the shots fired per interval
    //int shotsPerInterval;

    //[Tooltip("Strength of force with which the projectile is shot")]
    //[Range(1f, 3000f)]
    //public float _shotForce;

    //the force of the projectile shot
    //float shotForce;
    #endregion

    protected virtual void Awake()
    {
        if (thisEnemy != null)
        {
            return;
        }
        else
        {
            thisEnemy = GetComponent<Enemy>();
        }

        if (myRb != null)
        {
            return;
        }
        else
        {
            myRb = GetComponent<Rigidbody>();
        }
    }

    /// <summary>
    /// To turn the enemy upon encroaching on the edge of the platform 
    /// </summary>
    public void Turn() { }
    /// <summary>
    /// Attack method used when player is within range of melee attack
    /// </summary>
    public abstract void AttackPlayer();
    /// <summary>
    /// The Idle animation 
    /// </summary>
    public abstract void Idle();
    /// <summary>
    /// Triggers damage animation etc.
    /// </summary>
    public abstract void TookDamage();

    protected virtual void SetEnemyBehaviorState(EnemyBehavior enemyState)
    {
        enemyBehavior = enemyState;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LeftEdge")
        {
            Turn();
        }
        else if (other.gameObject.tag == "RightEdge")
        {
            Turn();
        }
    }
}