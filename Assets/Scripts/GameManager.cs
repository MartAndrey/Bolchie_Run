using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All states of the game.
public enum GameState { Menu, InGame, GameOver, Pause }

public class GameManager : MonoBehaviour
{
    //Current game state and the game starts in the menu
    public GameState currentGameState = GameState.Menu;

    //Singleton GameManager
    public static GameManager sharedInstance;

    //Reference to PlayerController
    PlayerController controller;

    public int collectedObject = 0;

    //Awake is called at the start of the first frame and before the Start method
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
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
        if (currentGameState == GameState.InGame)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    //Method in charge of the game
    public void StartGame()
    {
        SetGameState(GameState.InGame);
    }

    //Method in charge of returning to the menu
    public void BackToMenu()
    {
        SetGameState(GameState.Menu);
    }

    //Method in charge of finishing the game
    public void GameOver()
    {
        SetGameState(GameState.GameOver);
    }

    public void Pause()
    {
        SetGameState(GameState.Pause);
    }

    //Method responsible for modifying the current state of the game
    void SetGameState(GameState newGameState)
    {
        GameObject soundBackground = GameObject.Find("Sound Background");

        if (newGameState == GameState.Menu)
        {
            soundBackground.GetComponent<AudioSource>().Pause();

            MenuManager.sharedInstance.HideGameMenu();

            MenuManager.sharedInstance.HideDeathMenu();

            MenuManager.sharedInstance.HidePauseMenu();

            MenuManager.sharedInstance.ShowMainMenu();
        }
        else if (newGameState == GameState.InGame)
        {
            controller.StartGame();

            soundBackground.GetComponent<AudioSource>().Play();

            LevelManager.sharedInstance.RemoveAllLevelBlocks(); //Remove the blocks from the scene in case there are

            LevelManager.sharedInstance.GenerateInitialBlocks(); //Generate the initial blocks of the scene

            CameraFollow.sharedInstance.ResetPosition(); //Reset camera position

            MenuManager.sharedInstance.HideMainMenu();

            MenuManager.sharedInstance.HideDeathMenu();

            MenuManager.sharedInstance.ShowGameMenu();
        }
        else if (newGameState == GameState.GameOver)
        {
            soundBackground.GetComponent<AudioSource>().Pause();

            MenuManager.sharedInstance.HideGameMenu();

            MenuManager.sharedInstance.HideGameMenu();

            MenuManager.sharedInstance.ShowDeathMenu();
        }
        else if (newGameState == GameState.Pause)
        {
            if (MenuManager.sharedInstance.pauseCanvas.enabled == false)
            {
                soundBackground.GetComponent<AudioSource>().Pause();

                MenuManager.sharedInstance.pauseCanvas.enabled = true;
            }
            else
            {
                MenuManager.sharedInstance.pauseCanvas.enabled = false;

                newGameState = GameState.InGame;

                soundBackground.GetComponent<AudioSource>().Play();
            }
        }

        this.currentGameState = newGameState;
    }

    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;
    }
}
