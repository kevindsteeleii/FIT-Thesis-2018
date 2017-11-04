
using System;
using UnityEngine;

public enum GameState { pause, win, gameOver, inGame, menu};

public class GameManager : Singleton<GameManager>
{
    //stats, player movement and death state are handled in separate singletons
    public float levelStartDelay = 2f;
    //private int level = 1;
    public bool inputAllowed = true;

    //default state of game is the opening menu, incoming
    public GameState gameState = GameState.menu;

    /// <summary>
    /// Event that broadcasts upon restart after death.
    /// </summary>
    public event Action Restarting;

    // Use this for initialization
    protected virtual void Start()
    {
        //Used to alert the GameOver 
        PlayerStats.instance.HPisZero += GameOver;

        //upon respawned
        PlayerController.instance.Respawned += ResetGame;

        //subscribes the pause game inputs to game state setting methods
        PauseGame.instance.Paused += Pause;
        PauseGame.instance.Unpaused += UnPause;
        PauseGame.instance.Restarted += StartGame;

        StartGame();
        DontDestroyOnLoad(this);
    }

    protected virtual void Update()
    {
        Debug.Log("Current State is " + gameState);
    }

    public GameState GetState()
    {
        return gameState;
    }

    protected virtual void SetGameState(GameState newGameState)
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
        //PrintState();
    }

    //game at initialization
    protected virtual void StartGame()
    {
        Time.timeScale = 1;
        SetGameState(GameState.inGame);
    }

    /// <summary>
    /// Upon Reset an event is launched to pertinent listeners.
    /// </summary>
    protected virtual void ResetGame()
    {
        SetGameState(GameState.inGame);
        if (Restarting != null)
        {
            Restarting();
        }
    }

    //death sate renders hp to zero
    protected virtual void GameOver()
    {
        Time.timeScale = 0;
        SetGameState(GameState.gameOver);
        //PlayerStats.instance.hp = 0;
    }

    //protected void BackToMenu()
    //{
    //    SetGameState(GameState.menu);
    //}

    //will refactor the pause and unpause methods later, going to sleep < Kev note
    protected virtual void Pause()
    {
        Time.timeScale = 0;
        SetGameState(GameState.pause);
    }

    protected virtual void UnPause ()
    {
        Time.timeScale = 1;
        SetGameState(GameState.inGame);
    }

    protected void PrintState()
    {
        Debug.Log("Current State is "+gameState);
    }
}

