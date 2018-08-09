using System;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour {
    
    public List<GameObject> gameObjects { get; private set; }

    public event Action<Enemy> OnNewEnemy;
    public event Action<Player> OnNewPlayer;
    public Enemy enemyPreFab;
    public Player playerPrefab;

    public Enemy InstantiateEnemy(Vector3 enemyPosition) {
        Enemy spawnedEnemy = Instantiate(enemyPreFab, enemyPosition, Quaternion.identity) as Enemy;
        gameObjects.Add(spawnedEnemy.gameObject);
        OnNewEnemy(spawnedEnemy);
        return spawnedEnemy;
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
