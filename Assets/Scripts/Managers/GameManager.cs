
using System;
using UnityEngine;

public enum GameState { pause, win, gameOver, inGame, menu};

public class GameManager : Singleton<GameManager>
{
    //stats, player movement and death state are handled in separate singletons
    public float levelStartDelay = 2f;
    private int level = 1;
    public bool inputAllowed = true;

    //default state of game is the opening menu, incoming
    public GameState gameState = GameState.menu;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartGame();

        //death upon touching a insta-kill object
        //PlayerStats.instance.TouchDeath += GameOver;

        //Used to alert the GameOver 
        PlayerStats.instance.HPisZero += GameOver;

        //upon respawned
        PlayerController.instance.Respawned += ResetGame;

        //subscribes the pause game inputs to game state setting methods
        PauseGame.instance.Paused += Pause;
        PauseGame.instance.Unpause += UnPause;
    }

    private void Update()
    {
        Debug.Log("Current State is " + gameState);
    }

    public GameState GetState()
    {
        return gameState;
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
        //PrintState();
    }

    protected void StartGame()
    {
        SetGameState(GameState.inGame);
        Time.timeScale = 1;
    }

    protected void ResetGame()
    {
        SetGameState(GameState.inGame);
        PlayerStats.instance.ResetHP();
        PlayerController.instance.ReSpawn();
    }

    //death sate renders hp to zero
    protected void GameOver()
    {
        PauseUnpause();
        SetGameState(GameState.gameOver);
        //PlayerStats.instance.hp = 0;
    }

    protected void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    protected void Pause()
    {
        PauseUnpause();
        SetGameState(GameState.pause);
    }

    protected void UnPause ()
    {
        PauseUnpause();
        SetGameState(GameState.inGame);
    }

    protected void PrintState()
    {
        Debug.Log("Current State is "+gameState);
    }

    /// <summary>
    /// Private method used to toggle in-game time exclusively here no REPEATS!!!
    /// Houses all gamestate and time logic here!!
    /// </summary>
    protected void PauseUnpause()
    {
        if (Time.timeScale == 0 && gameState == GameState.gameOver || gameState == GameState.pause)
        {
            Time.timeScale = 1;
        }
        else if (Time.timeScale == 1 &&  gameState == GameState.inGame)
        {
            Time.timeScale = 0;
        }
    }
}

