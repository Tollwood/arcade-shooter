using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    
    public Action OnGameOver;
    public Action OnNewGame;
    public event Action<Level> OnNewLevel;

    public Player playerPrefab;
    public Player player { get; private set; }

    public List<Level> levels;
    Level currentLevel;

    // TODO move to remainingTime Feature
    public float remainingTime { get; private set; }
    public bool playing { get; private set; }

    private void Update()
    {
        if(playing){
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0 && levels.IndexOf(currentLevel) < levels.Count)
            {
                NextLevel();
            }    
        }
    }

    public void GameOver(){
        playing = false;
        currentLevel = null;
        OnGameOver();
    }

    public void NewGame(){
        player =  Instantiate(playerPrefab, transform);
        player.onDeath += GameOver;
        NextLevel();
        // TODO pass player to OnNewGame
        OnNewGame();
        playing = true;
    }
  
    void NextLevel()
    {
        currentLevel = GetNextLevel();
        if (currentLevel != null)
        {
            remainingTime = currentLevel.spawnTime;
            ResetPlayerPosition();
            if (OnNewLevel != null)
            {
                OnNewLevel(currentLevel);
            }
        }
    }

    private Level GetNextLevel()
    {
        if(currentLevel == null){
            return levels[0];
        }
        if(levels.IndexOf(currentLevel) +1 < levels.Count){
            return levels[levels.IndexOf(currentLevel) + 1];
        }
        return null;
    }

    void ResetPlayerPosition()
    {
        player.transform.position = FindObjectOfType<MapGenerator>().GetTileFromPosition(Vector3.zero).position + Vector3.up * 3;
    }

    internal void increaseRemainingTime(float timeIncrease)
    {
        remainingTime += timeIncrease;
    }
}
