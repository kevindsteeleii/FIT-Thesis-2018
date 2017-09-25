
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum GameState { pause, win, gameOver, inGame, menu };

public class GameManager : Singleton<GameManager>
{

    //money, hp and the like are all handled in different singletons
    public float levelStartDelay = 2f;
    private int level = 1;
    public bool isPaused = false;
    public bool inputAllowed = true;

    //default state of game is the opening menu, incoming
    public GameState gameState = GameState.menu;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameState = GameState.inGame;
    }

    void SetGameState(GameState newGameState)
    {
        /*different gameStates should prompt the visibility of pertinent canvases
         and invisibility of all others/obscure them*/

        switch (newGameState)
        {
            //write the associated functions/methods later
            case GameState.inGame:
                //setup Unity scene for inGame state
                break;
            case GameState.gameOver:
                //setup Unity scene for gameOver state
                break;
            case GameState.pause:
                //setup Unity scene for pause state
                break;
            case GameState.win:
                break;
        }
        gameState = newGameState;
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    public void Pause()
    {
        SetGameState(GameState.pause);
    }

}
