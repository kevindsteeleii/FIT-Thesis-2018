
using UnityEngine;

public enum GameState { pause, win, gameOver, inGame, menu};

public class GameManager : Singleton<GameManager>
{
    //stats, player movement and death state are handled in separate singletons
    public float levelStartDelay = 2f;
    private int level = 1;
    public bool isPaused = false;
    public bool inputAllowed = true;
    public bool isDead { get; set;}

    //default state of game is the opening menu, incoming
    public GameState gameState = GameState.menu;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartGame();
        isDead = false;

        //death upon health at zero
        //PlayerStats.instance.HpChanged += GameOverSub;

        //death upon touching a insta-kill object
        PlayerStats.instance.TouchDeath += GameOver;

        //Used to alert the GameOver 
        PlayerStats.instance.HPisZero += GameOver;

        //upon respawned
        //PlayerController.instance.ReSpawned += ResetGame;
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
        PrintState();
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
        Time.timeScale = 1;
        isDead = false;
    }

    public void ResetGame()
    {
        SetGameState(GameState.inGame);
        PlayerStats.instance.ResetHP();
        PlayerController.instance.ReSpawn();
        isDead = false;
    }

    //death sate renders hp to zero
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
        PlayerStats.instance.hp = 0;
        isDead = true;
    }

    //Subscriber used to trigger the death state
    //void GameOverSub(int health)
    //{
    //    if (health <= 0)
    //    {
    //        GameOver();
    //    }
    //}

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    public void Pause()
    {
        SetGameState(GameState.pause);
        Time.timeScale = 0;
    }

    void PrintState()
    {
        Debug.Log("Current State is "+gameState);
    }
}
