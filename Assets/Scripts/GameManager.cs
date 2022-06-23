using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All states of the game.
public enum GameState { Menu, InGame, GameOver }

public class GameManager : MonoBehaviour
{
    //Current game state and the game starts in the menu
    public GameState currentGameState = GameState.Menu;

    //Singleton GameManager
    public static GameManager sharedInstanceGM;

    //Reference to PlayerController
    PlayerController controller;

    //// Awake is called at the start of the first frame and before the Start method
    void Awake()
    {
        if (sharedInstanceGM == null)
        {
            sharedInstanceGM = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.InGame)// if the game state is other than "InGame", the game will be paused
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }

        if (Input.GetButtonDown("Submit") && currentGameState != GameState.InGame)
        {
            StartGame();
        }
    }

    //Method in charge of the game
    public void StartGame()
    {
        SetGameState(GameState.InGame);
    }

    //Method in charge of finishing the game
    public void GameOver()
    {
        SetGameState(GameState.GameOver);
    }

    //Method in charge of returning to the menu
    public void BackToMenu()
    {
        SetGameState(GameState.Menu);
    }

    //Method responsible for modifying the current state of the game
    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.Menu)
        {
            //TODO: Set menu logic 
        }
        else if (newGameState == GameState.InGame)
        {
            controller.StrartGame(); 
        }
        else if (newGameState == GameState.GameOver)
        {
            //TODO: Set the game to game over
        }

        this.currentGameState = newGameState;
    }
}
