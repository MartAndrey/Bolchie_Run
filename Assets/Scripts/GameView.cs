using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameView : MonoBehaviour
{
    public TextMeshProUGUI coinsText, scoreText, highScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.InGame)
        {
            int coins = GameManager.sharedInstance.collectedObject;
            float score = PlayerController.sharedInstance.GetTravelledDistance();
            float highScore = PlayerPrefs.GetFloat("High Score", 0);

            coinsText.text = coins.ToString();
            scoreText.text = score.ToString("f1");
            highScoreText.text = highScore.ToString("f1");
        }
    }
}
