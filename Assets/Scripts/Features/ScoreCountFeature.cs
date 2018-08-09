using UnityEngine;

public class ScoreCountFeature : MonoBehaviour
{
    private static string HIGH_SCORE = "highscore";
    public int score { get; private set; }
    public int highScore { get; private set; }

    private void Awake()
    {
        highScore = PlayerPrefs.GetInt(HIGH_SCORE, 0);
        score = -1;
    }

    void Start () {
        Spawner spawner = FindObjectOfType<Spawner>();
        spawner.OnNewEnemy += OnNewEnemy;
        Game gm = FindObjectOfType<Game>();
        gm.OnNewGame += OnNewGame;
        gm.OnGameOver += OnGameOver;
	}

    public void OnNewEnemy(Enemy enemy){
        enemy.onDeath += OnEnemyKilled;
    }
	
    public void OnEnemyKilled(){
        score += 5;
    }

    private void OnNewGame() { 
        score = 0;
    }

    private void OnGameOver(){
        if (score > highScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE, score);
        }
    }

}
