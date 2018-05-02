using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerAnimParams {Throwing, attacking, dead, grounded, vertSpeed, attackChain, slam, toss, aiming, grabbing};
/// <summary>
/// Component Used to activate and the hitboxes attached to the same object as script and change the object's tag as well
/// </summary>
public class PlayerHitBoxControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
[System.Serializable]
public class PlayerAnimationParameterComparer
{
    public PlayerAnimParams myAnimParam;
    public ComparativeOperator myCompOp;
    public bool evaluation;
    public float num;

    public bool GetArgument (Animator anim)
    {
        bool eval = false;
        return eval;
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