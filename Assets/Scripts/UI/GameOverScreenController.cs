using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenController : MonoBehaviour {
    
    public Image gameOverBG;
    public GameObject gameOverMenu;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI currentScoreTitle;
    public TextMeshProUGUI currentScoreValue;
    public TextMeshProUGUI gameOverText;
    private bool gameOver = false;
    private ScoreCounter scoreCounter;

    private void Start (){
        Game gm = FindObjectOfType<Game>();
        gm.OnGameOver += OnGameOver;
        gm.OnGameOver += () => { gameOver = true; };
        gm.OnNewGame += OnNewGame;
        scoreCounter = FindObjectOfType<ScoreCounter>();
        highScore.text = "" + scoreCounter.highScore;
    }

    private void Update(){
        
        if (!gameOver)
        {
            currentScoreTitle.enabled = false;
            currentScoreValue.enabled = false;
            gameOverText.enabled = false;
        }
        else
        {
            currentScoreTitle.enabled = true;
            currentScoreValue.enabled = true;
            gameOverText.enabled = true;
            currentScoreValue.text = "" + scoreCounter.score;
        }

        highScore.text = "" + scoreCounter.highScore;
    }

    private void OnGameOver()
    {
        Color tmpColor = gameOverBG.color;
        tmpColor.a = 150;
        gameOverBG.color = tmpColor;
        gameOverMenu.SetActive(true);
    }

    private void OnNewGame()
    {
        Color tmpColor = gameOverBG.color;
        tmpColor.a = 0;
        gameOverBG.color = tmpColor;
        gameOverMenu.SetActive(false);
    }

}
