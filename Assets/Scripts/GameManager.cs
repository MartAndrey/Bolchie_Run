using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All states of the game.
public enum GameState { menu, inGame, gameOver }

public class GameManager : MonoBehaviour
{
    //Current game state and the game starts in the menu
    public GameState currentGameState = GameState.menu;

    //Singleton GameManager
    public static GameManager sharedInstanceGM;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Method in charge of the game
    public void StartGame()
    {

    }

    //Method in charge of finishing the game
    public void GameOver()
    {

    }

    //Method in charge of returning to the menu
    public void BackToMenu()
    {

    }

    //Method responsible for modifying the current state of the game
    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            //TODO: Set menu logic 
        }
        else if (newGameState == GameState.inGame)
        {
            //TODO: Set the scene to play
        }
        else if (newGameState == GameState.gameOver)
        {
            //TODO: Set the game to game over
        }

        this.currentGameState = newGameState;
    }
}
