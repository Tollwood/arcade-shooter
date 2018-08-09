using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CampingFeature))]
[RequireComponent(typeof(SpawnTimeFeature))]
public class Spawner : MonoBehaviour
{
    public event Action<Enemy> OnNewEnemy;
    // TODO All game Entities should be Instantiated within on parent and this one should remove children once game is over
    private List<Enemy> livingEnemies;
    public Enemy enemyPreFab;

    private MapGenerator map;
    float nextSpawnTime;

    CampingFeature campingFeature;
    SpawnTimeFeature spawnTimeFeature;
    Game gm;

    private void Start()
    {
        map = FindObjectOfType<MapGenerator>();
        gm = FindObjectOfType<Game>();
        gm.OnNewLevel += OnNewLevel;
        gm.OnGameOver += OnGameOver;
        campingFeature = GetComponent<CampingFeature>();
        spawnTimeFeature = GetComponent<SpawnTimeFeature>();
        livingEnemies = new List<Enemy>();
    }

    void Update () {
        if(gm.playing && Time.time > nextSpawnTime ){
            nextSpawnTime = spawnTimeFeature.NextSpawnTime();
            campingFeature.checkCamping(gm.player);
            StartCoroutine(SpawnEnemy());
        }
	}

    IEnumerator SpawnEnemy(){
        float spawnDelay = 1;
        float tileFlashSpeed = 4;
        Color flashColour = Color.red;

        Transform spawnTile = map.GetFreeTilePosition();
        if (campingFeature.enabled && campingFeature.isCamping)
        {
            spawnTile = map.GetTileFromPosition(gm.player.transform.position);
        }
        Material tileMat = spawnTile.GetComponent<Renderer>().material;
        Color initialColour = tileMat.color;

        float spawnTimer = 0;
        while (spawnTimer < spawnDelay)
        {
            tileMat.color = Color.Lerp(initialColour, flashColour, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1));
            spawnTimer += Time.deltaTime;
            yield return null;
        }

        Enemy spawnedEnemy = Instantiate(enemyPreFab, spawnTile.position + Vector3.up, Quaternion.identity) as Enemy;
        OnNewEnemy(spawnedEnemy);
        livingEnemies.Add(spawnedEnemy);

    }


    private void OnNewLevel(Level level)
    {
        removeLivingEnemies();
    }

    private void OnGameOver()
    {
        removeLivingEnemies();
    }

    private void removeLivingEnemies()
    {
        foreach (Enemy enemy in livingEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}
