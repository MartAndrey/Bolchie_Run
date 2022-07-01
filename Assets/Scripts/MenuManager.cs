using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;

    public Canvas menuCanvas, gameCanvas, deathCanvas, pauseCanvas;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }

        gameCanvas.enabled = false;

        deathCanvas.enabled = false;

        pauseCanvas.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            GameManager.sharedInstance.Pause();
        }
    }

    public void ShowMainMenu()
    {
        menuCanvas.enabled = true;
    }

    public void HideMainMenu()
    {
        menuCanvas.enabled = false;
    }

    public void ShowGameMenu()
    {
        gameCanvas.enabled = true;
    }

    public void HideGameMenu()
    {
        gameCanvas.enabled = false;
    }

    public void ShowDeathMenu()
    {
        deathCanvas.enabled = true;
    }

    public void HideDeathMenu()
    {
        deathCanvas.enabled = false;
    }

    public void ShowPauseMenu()
    {
        deathCanvas.enabled = true;
    }

    public void HidePauseMenu()
    {
        deathCanvas.enabled = false;
    }

    public void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}


