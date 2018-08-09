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
    private bool showCurrentScore = false;
    private ScoreCountFeature scoreCounter;

    private void Start (){
        Game gm = FindObjectOfType<Game>();
        gm.OnGameOver += OnGameOver;
        gm.OnGameOver += () => { showCurrentScore = true; };
        gm.OnNewGame += OnNewGame;
        scoreCounter = FindObjectOfType<ScoreCountFeature>();
        highScore.text = "" + scoreCounter.highScore;
    }

    private void Update(){
        
        if (!showCurrentScore)
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
