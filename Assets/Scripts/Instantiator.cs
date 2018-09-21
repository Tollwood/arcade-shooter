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

    public GameObject Instantiate(Spawnable spawnable, Vector3 spawnPosition) {
        switch(spawnable){
            case Spawnable.Enemy:
                return InstantiateEnemy(spawnPosition);
            case Spawnable.HealthBoost:
                return InstantiateHealthBoost(spawnPosition);

            case Spawnable.TimeBoost:
                return InstantiateTimeBoost(spawnPosition);
            case Spawnable.SpawnBoost:
                return  InstantiateSpawnBoost(spawnPosition);
            default:
                throw new Exception("Spawnable is not configured");
        }

    }

    private GameObject InstantiateEnemy(Vector3 spawnPosition)
    {
        Enemy spawnedEnemy = Instantiate(enemyPreFab, spawnPosition, Quaternion.identity) as Enemy;
        gameObjects.Add(spawnedEnemy.gameObject);
        OnNewEnemy(spawnedEnemy);
        return spawnedEnemy.gameObject;
    }

    private GameObject InstantiateHealthBoost(Vector3 spawnPosition)
    {
        HealthBoost healthBoost = Instantiate(healthBoostPreFab, spawnPosition, Quaternion.identity);
        gameObjects.Add(healthBoost.gameObject);
        return healthBoost.gameObject;
    }

    private GameObject InstantiateTimeBoost(Vector3 spawnPosition)
    {
        TimeBoost timeBoost = Instantiate(timeBoostPreFab, spawnPosition, Quaternion.identity);
        gameObjects.Add(timeBoost.gameObject);
        return timeBoost.gameObject;
    }

    private GameObject InstantiateSpawnBoost(Vector3 spawnPosition){
        SpawnBoost spawnBoost = Instantiate(spawnBoostPreFab, spawnPosition, Quaternion.identity);
        gameObjects.Add(spawnBoost.gameObject);
        return spawnBoost.gameObject;
    }

    public Player InstantiatePlayer(Vector3 playerPosition)
    {
        Player spwanedPlayer = Instantiate(playerPrefab, playerPosition, Quaternion.identity) as Player;
        gameObjects.Add(spwanedPlayer.gameObject);
        if(OnNewPlayer != null){
            OnNewPlayer(spwanedPlayer);    
        }
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
