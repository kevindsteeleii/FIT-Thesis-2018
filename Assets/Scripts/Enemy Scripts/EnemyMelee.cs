using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyAI {

    // Use this for initialization
    void Start () {
        SetEnemyBehaviorState(EnemyBehavior.Patrolling);
	}
	
	// Update is called once per frame
	void Update () {
        switch (enemyBehavior)
        {
            case EnemyBehavior.Stationary:
                Idle();
                break;
            case EnemyBehavior.Patrolling:
                Patrolling();
                break;
            case EnemyBehavior.Pursuing:
                InPursuit();
                break;
            case EnemyBehavior.Attacking:
                AttackPlayer();
                break;
            case EnemyBehavior.Shooting:
                enemyBehavior = EnemyBehavior.Stationary;
                break;
            case EnemyBehavior.Turret:
                enemyBehavior = EnemyBehavior.Stationary;
                break;
            case EnemyBehavior.Floating:
                enemyBehavior = EnemyBehavior.Stationary;
                break;
            case EnemyBehavior.Dive_Bombing:
                enemyBehavior = EnemyBehavior.Stationary;
                break;
            default:
                break;
        }
    }

    private void InPursuit()
    {
        throw new NotImplementedException();
    }

    private void Patrolling()
    {
        throw new NotImplementedException();
    }

    private void FixedUpdate()
    {

    }

    protected override void SetEnemyBehaviorState(EnemyBehavior enemyState)
    {
        base.SetEnemyBehaviorState(enemyState);
    }

    public override void TookDamage()
    {
        throw new System.NotImplementedException();
    }

    public override void AttackPlayer()
    {
        throw new System.NotImplementedException();
    }

    public override void Idle()
    {
        Debug.Log("Enemy is in idle");
    }
}
