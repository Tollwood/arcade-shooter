using System;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour {
    
    public List<GameObject> gameObjects { get; private set; }

    public event Action<Enemy> OnNewEnemy;
    public event Action<Player> OnNewPlayer;
    public Enemy enemyPreFab;
    public HealthBoost healthBoostPreFab;
    public TimeBoost timeBoostPreFab;
    public SpawnBoost spawnBoostPreFab;
    public Player playerPrefab;

    public void Instantiate(Spawnable spawnable, Vector3 spawnPosition) {
        switch(spawnable){
            case Spawnable.Enemy:
                InstantiateEnemy(spawnPosition);
                break;
            case Spawnable.HealthBoost:
                InstantiateHealthBoost(spawnPosition);
                break;
            case Spawnable.TimeBoost:
                InstantiateTimeBoost(spawnPosition);
                break;
            case Spawnable.SpawnBoost:
                InstantiateSpawnBoost(spawnPosition);
                break;
        }
    }

    private void InstantiateEnemy(Vector3 spawnPosition)
    {
        Enemy spawnedEnemy = Instantiate(enemyPreFab, spawnPosition, Quaternion.identity) as Enemy;
        gameObjects.Add(spawnedEnemy.gameObject);
        OnNewEnemy(spawnedEnemy);  
    }

    private void InstantiateHealthBoost(Vector3 spawnPosition)
    {
        HealthBoost healthBoost = Instantiate(healthBoostPreFab, spawnPosition, Quaternion.identity);
        gameObjects.Add(healthBoost.gameObject);
    }

    private void InstantiateTimeBoost(Vector3 spawnPosition)
    {
        TimeBoost timeBoost = Instantiate(timeBoostPreFab, spawnPosition, Quaternion.identity);
        gameObjects.Add(timeBoost.gameObject);
    }

    private void InstantiateSpawnBoost(Vector3 spawnPosition){
        SpawnBoost spawnBoost = Instantiate(spawnBoostPreFab, spawnPosition, Quaternion.identity);
        gameObjects.Add(spawnBoost.gameObject);
    }

    public Player InstantiatePlayer(Vector3 playerPosition)
    {
        Player spwanedPlayer = Instantiate(playerPrefab, playerPosition, Quaternion.identity) as Player;
        gameObjects.Add(spwanedPlayer.gameObject);
        OnNewPlayer(spwanedPlayer);
        return spwanedPlayer;
    }

    private void Start()
    {
        gameObjects = new List<GameObject>();
        Game gm = FindObjectOfType<Game>();
        gm.OnGameOver += removeAllGameObjects;
        gm.OnNewLevel += OnNewLevel;
    }

    private void OnGameOver(){
        removeAllGameObjects();
    }

    private void OnNewLevel(Level level)
    {
        removeAllGameObjects();
    }
    private void removeAllGameObjects()
    {
        foreach (GameObject go in gameObjects)
        {
            if (go != null)
            {
                Destroy(go.gameObject);
            }
        }
    }
}
