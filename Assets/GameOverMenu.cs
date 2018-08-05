using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour {

    private ScoreCounter scoreCounter;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI currentScoreTitle;
    public TextMeshProUGUI currentScoreValue;
    public TextMeshProUGUI gameOverText;
    private bool gameOver = false;

    private void Start()
    {
        Game gm = FindObjectOfType<Game>();
        gm.OnGameOver += () => { gameOver = true; };
    }
    private void OnEnable()
    {
        scoreCounter = FindObjectOfType<ScoreCounter>();
        if(!gameOver ){
            currentScoreTitle.enabled = false;
            currentScoreValue.enabled = false;
            gameOverText.enabled = false;
        }
        else {
            currentScoreTitle.enabled = true;
            currentScoreValue.enabled = true;
            gameOverText.enabled = true;
            currentScoreValue.text = ""+ scoreCounter.score;
        }

        highScore.text = "" + scoreCounter.highScore;
	}
}
