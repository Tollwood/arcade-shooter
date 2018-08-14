using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Instantiator))]
public class Game : MonoBehaviour {
    
    public Action OnGameOver;
    public Action OnNewGame;
    public event Action<Level> OnNewLevel;

    Instantiator instantiator;

    public List<Level> levels;
    Level currentLevel;

    // TODO move to remainingTime Feature
    public float remainingTime { get; private set; }
    public bool playing { get; private set; }

    private void Start()
    {
        instantiator = FindObjectOfType<Instantiator>();
    }

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

    public void NewGame(){
        NextLevel();
        OnNewGame();
        playing = true;
    }

    public void GameOver()
    {
        playing = false;
        currentLevel = null;
        OnGameOver();
    }

    void NextLevel()
    {
        currentLevel = GetNextLevel();
        if (currentLevel != null)
        {
            remainingTime = currentLevel.spawnTime;
            if (OnNewLevel != null)
            {
                OnNewLevel(currentLevel);
                Vector3 playerPosition = FindObjectOfType<MapGenerator>().GetTileFromPosition(Vector3.zero).position + Vector3.up * 3;
                Player player = instantiator.InstantiatePlayer(playerPosition);
                player.onDeath += GameOver;
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

    internal void increaseRemainingTime(float timeIncrease)
    {
        remainingTime += timeIncrease;
    }

}
