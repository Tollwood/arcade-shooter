using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Wave[] waves;
    Wave currentWave;
    int currentWaveNumber;
    public event Action<int> OnNewWave;
    public event Action<Enemy> OnNewEnemy;
    private List<Enemy> livingEnemies;
    public Enemy enemyPreFab;

    private MapGenerator map;

    Player player;

    public float remainingTimeToSpawn { get; private set; }
    float nextSpawnTime;
    float currentTimeBetweenSpan;

    float timeBetweenCampingChecks = 2;
    float campThresholdDistance = 1.5f;
    float nextCampCheckTime;
    Vector3 campPositionOld;
    bool isCamping;

    private void Start()
    {
        map = FindObjectOfType<MapGenerator>();
        Game gm = FindObjectOfType<Game>();
        gm.OnNewGame += ResetGame;
        livingEnemies = new List<Enemy>();
    }

    private Player GetPlayer(){
        if(player == null){
            player = FindObjectOfType<Player>();
        }
        return player;
    }

    void Update () {
        if(player == null){
            return;
        }
        remainingTimeToSpawn -= Time.deltaTime;
        if (Time.time > nextCampCheckTime)
        {
            nextCampCheckTime = Time.time + timeBetweenCampingChecks;

            isCamping = (Vector3.Distance(player.transform.position, campPositionOld) < campThresholdDistance);
            campPositionOld = player.transform.position;
        }
        if(remainingTimeToSpawn > 0 && Time.time > nextSpawnTime ){
            nextSpawnTime = Time.time + currentTimeBetweenSpan;
            StartCoroutine(SpawnEnemy());
        }
        if (remainingTimeToSpawn < 0 && currentWaveNumber <= waves.Length)
        {
            NextWave();
        }
	}

    IEnumerator SpawnEnemy(){
        float spawnDelay = 1;
        float tileFlashSpeed = 4;

        Transform spawnTile = map.GetFreeTilePosition();
        if (isCamping)
        {
            spawnTile = map.GetTileFromPosition(player.transform.position);
        }
        Material tileMat = spawnTile.GetComponent<Renderer>().material;
        Color initialColour = tileMat.color;
        Color flashColour = Color.red;
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
        currentTimeBetweenSpan -= currentWave.timeIncrement;
    }


    void ResetPlayerPosition()
    {
        player.transform.position = map.GetTileFromPosition(Vector3.zero).position + Vector3.up * 3;
    }

    void NextWave()
    {
        Player currentPlayer = GetPlayer();
        if (currentPlayer == null)
        {
            return;
        }

        foreach(Enemy enemy in livingEnemies)
        {
            if(enemy != null){
                Destroy(enemy.gameObject);
            }
        }

        currentWaveNumber++;

        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            currentTimeBetweenSpan = currentWave.timeBetweenSpan;
            remainingTimeToSpawn = currentWave.spawnTime;


            if (OnNewWave != null)
            {
                OnNewWave(currentWaveNumber);
            }
            ResetPlayerPosition();
        }
    }
 
    public void ResetGame(){
        currentWaveNumber = 0;
        NextWave();
    }

    [System.Serializable]
    public class Wave
    {
        public float spawnTime;
        public float timeBetweenSpan;
        public float timeIncrement;
    }
}
