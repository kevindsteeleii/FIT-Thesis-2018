
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { pause, win, gameOver, inGame, menu };

public class GameManager : Singleton<GameManager>
{
    #region Global Variables
    //stats, player movement and death state are handled in separate singletons
    public float levelStartDelay = 2f;
    //private int level = 1;
    public bool inputAllowed = true;

    //default state of game is the opening menu, incoming
    public GameState gameState = GameState.menu;

    /// <summary>
    /// Event that broadcasts from Game Manager upon restart after death.
    /// </summary>
    public event Action onRestartState;

    /// <summary>
    /// Event that broadcasts Death state from the game manager upon death.
    /// </summary>
    public event Action onGameOverState;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(this);
    }

    // Use this for initialization
    protected virtual void Start()
    {
        //Used to alert the GameOver 
        PlayerStats.instance.On_ZeroHP_Sent += OnZeroHP;
        //subscribes the pause game inputs to game state setting methods
        GUIManager.instance.On_PauseButton_Sent += OnPauseButton;
        GUIManager.instance.On_ResumeButton_Sent += OnResumeButton;
        GUIManager.instance.onRestartButton += OnRestartButton;

        StartGame();
    }

    public virtual void Update()
    {
        //when game state is gameover an event is broadcast to the appropriate subscribers 
        if (gameState == GameState.gameOver && onGameOverState != null)
        {
            onGameOverState();
            GameOver();
        }
    }

    public GameState GetState()
    {
        return gameState;
    }

    public virtual void SetGameState(GameState newGameState)
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
        Debug.Log("Game State is: " + newGameState);
        gameState = newGameState;
    }

    //game at initialization
    void StartGame()
    {
        Time.timeScale = 1;
        SetGameState(GameState.inGame);
    }

    /// <summary>
    /// Upon Reset an event is launched to pertinent listeners.
    /// </summary>
    public virtual void OnRestartButton()
    {
        SetGameState(GameState.inGame);
        Time.timeScale = 1;

        if (onRestartState != null)
        {
            onRestartState();
        }
    }

    //death sate renders hp to zero
    public virtual void OnZeroHP()
    {
        Time.timeScale = 0;
        SetGameState(GameState.gameOver);
        //PlayerStats.instance.hp = 0;
    }

    public virtual void GameOver()
    {
        OnZeroHP();
    }

    //will refactor the pause and unpause methods later, going to sleep < Kev note
    public virtual void OnPauseButton()
    {
        Time.timeScale = 0;
        SetGameState(GameState.pause);
    }

    public virtual void OnResumeButton()
    {
        Console.WriteLine("UnPaused!!");
        Time.timeScale = 1;
        SetGameState(GameState.inGame);
    }
}