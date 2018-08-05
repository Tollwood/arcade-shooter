using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Wave[] waves;
    Wave currentWave;
    int currentWaveNumber;
    public event Action<int> OnNewWave;
    public event Action<Enemy> OnNewEnemy;

    public Enemy enemyPreFab;

    private MapGenerator map;

    Player player;

    int remainingEnemiesToSpawn;
    int livingEnemiesCount;
    float nextSpawnTime;

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
        if (Time.time > nextCampCheckTime)
        {
            nextCampCheckTime = Time.time + timeBetweenCampingChecks;

            isCamping = (Vector3.Distance(player.transform.position, campPositionOld) < campThresholdDistance);
            campPositionOld = player.transform.position;
        }
        if(remainingEnemiesToSpawn > 0 && Time.time > nextSpawnTime ){
            remainingEnemiesToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpan;
            StartCoroutine(SpawnEnemy());
        }	
        if(livingEnemiesCount == 0 && currentWaveNumber <= waves.Length){
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
        spawnedEnemy.onDeath += OnEnemyDeath;
        OnNewEnemy(spawnedEnemy);
    }

    void OnEnemyDeath()
    {
        livingEnemiesCount--;

        if (livingEnemiesCount == 0)
        {
            NextWave();
        }
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

        currentWaveNumber++;

        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            remainingEnemiesToSpawn = currentWave.enemyCount;
            livingEnemiesCount = remainingEnemiesToSpawn;

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
        public int enemyCount;
        public float timeBetweenSpan;
    }
}
