using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;

    public Canvas menuCanvas;

    public Canvas gameCanvas;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }

        gameCanvas.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

    public void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}


