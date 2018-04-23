using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
/// <summary>
/// Enum used to handle type of Animator Parameter for Animation state and parameter based conditional outcomes
/// </summary>
public enum AnimatorParameter {enemyDetected, meleeRange, speed, vertSpeed};
public enum ComparativeOperator { NA, EqualTo, NotEqualTo, LessThan, LessThanOrEqualTo, GreaterThan,GreaterThanOrEqualTo};
[Serializable]
public class EnemyMeleeHitBox : MonoBehaviour {
    public Animator myAnim; //animator's parameters used to determine the status of hitboxes
    public AnimParamComparer [] animParams;
    [Tooltip("All possible colliders associated with the attack script")]
    public Collider[] cols;
    bool hitting = false;

    /// <summary>
    /// Event used to adjust damage output for melee attacks based on number of multiple potential hitboxes at play
    /// </summary>
    public event Action<int> On_HitBoxNumber_Sent;

    // Use this for initialization
    void Start () {

        SetColliders(false);
    }

    void SetColliders( bool eval)
    {
        foreach (Collider item in cols)
        {
            item.gameObject.SetActive(eval);
        }
    }

    void Hitting()
    {
        foreach (AnimParamComparer comper in animParams)
        {
            Debug.Log(comper.myAnimParam);
            if (comper.GetArgument(myAnim))
            {
                hitting = true;
            }
            else
            {
                hitting = false;
                break;
            }
        }
        Debug.Log("Hitting is " + hitting);
        SetColliders(hitting);
    }
	// Update is called once per frame
	void Update () {
        Hitting();

        if (On_HitBoxNumber_Sent != null)
        {
            On_HitBoxNumber_Sent(cols.Length);
        }
    }
}
/// <summary>
/// Class used for singular arguments for the possible animations 
/// </summary>
[System.Serializable]   //making this class not derived from Monobehaviour allows its public stuff to be seen in inspectector and Serializable Attribute allows it to be seen/modified in inspector
public class AnimParamComparer
{
    public AnimatorParameter myAnimParam;
    public ComparativeOperator myCompOp;
    public bool evaluation;
    public float num;

    public bool GetArgument(Animator anim)
    {
        bool eval = false;

        switch (myAnimParam)
        {
            case AnimatorParameter.enemyDetected:
            case AnimatorParameter.meleeRange:
                myCompOp = ComparativeOperator.NA;
                eval = GetBoolAnim(anim);
                break;
            case AnimatorParameter.speed:
            case AnimatorParameter.vertSpeed:
                eval = GetNumberAnim(anim);
                break;
            default:
                break;
        }
        return eval;
    }

    bool GetBoolAnim (Animator anim)
    {
        string param = "";
        param = (myAnimParam == AnimatorParameter.enemyDetected) ? "enemyDetected" : "meleeRange";
        return (anim.GetBool(param) == evaluation);
    }

    bool GetNumberAnim (Animator anim)
    {
        bool answer = false;
        string parameter = "";
        parameter =  (myAnimParam == AnimatorParameter.speed)?"speed": "vertSpeed";
        switch (myCompOp)
        {
            case ComparativeOperator.EqualTo:
                answer = (anim.GetFloat(parameter) == num);
                break;
            case ComparativeOperator.NotEqualTo:
                answer = (anim.GetFloat(parameter) != num);
                break;
            case ComparativeOperator.LessThan:
                answer = (anim.GetFloat(parameter) < num);
                break;
            case ComparativeOperator.LessThanOrEqualTo:
                answer = (anim.GetFloat(parameter) <= num);
                break;
            case ComparativeOperator.GreaterThan:
                answer = (anim.GetFloat(parameter) > num);
                break;
            case ComparativeOperator.GreaterThanOrEqualTo:
                answer = (anim.GetFloat(parameter) >= num);
                break;
            default:
                answer = false;
                break;
        }
        return answer;
    }
    
}
#region TODO list, refactoring etc
/************TODO Refactoring********************************************************************//*
 1- Figure out how to code this as a melee hitbox params wizard
 2- Does not work for instances of class in another class
 3-
 4-
 *************************************************************************************************/
#endregion