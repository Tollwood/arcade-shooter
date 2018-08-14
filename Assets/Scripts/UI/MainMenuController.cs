using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI currentScoreTitle;
    public TextMeshProUGUI currentScoreValue;
    public TextMeshProUGUI gameOverText;
    private bool showCurrentScore = false;
    private ScoreCountFeature scoreCounter;

    private void Start (){
        Game gm = FindObjectOfType<Game>();
        gm.OnGameOver += () => { showCurrentScore = true; };
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
}
