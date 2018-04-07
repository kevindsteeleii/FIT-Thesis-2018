using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script that takes care fo some behavioral considerations of the animated GUI used in the pause and gameover states
/// </summary>
public class AnimatedSlidingMenu : MonoBehaviour {

    #region Global context variables
    Transform targetCamera; // used as reference as to where to aim the x,y of the sliding menu

    [Tooltip("X offset from camera center")]
    [Range(0f, 3f)]
    public float xOffSet = 0;

    [Tooltip("Y offset from camera center")]
    [Range(0f, 3f)]
    public float yOffSet = 0;
    #endregion

    // Use this for initialization
    void Start () {
		if (targetCamera == null)
        {
            targetCamera = FindObjectOfType<Camera>().transform;
        }
        else
        {
            return;
        }

        //subscribes the state changes of the game manager to specific 
        GameManager.instance.On_GameState_Sent += On_GameState_Received;
    }
	
	// Update is called once per frame
	void Update () {
        float newX = xOffSet + targetCamera.position.x;
        float newY = yOffSet + targetCamera.position.y;
        Vector3 newPos = gameObject.transform.position;
        newPos.y = newY;
        newPos.x = newX;
        gameObject.transform.position = newPos;
	}

    //a subscriber that uses a switch state to change the activity of the sliding menu object
    public void On_GameState_Received(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.pause:
            case GameState.gameOver:
                gameObject.SetActive(true);
                break;
            case GameState.win:
                break;
            case GameState.inGame:
                gameObject.SetActive(false);
                break;
            case GameState.menu:
                break;
            default:
                break;
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
