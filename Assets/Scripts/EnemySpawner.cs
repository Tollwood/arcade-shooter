using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    
    public Wave[] waves;
    public Enemy enemyPreFab;

    Wave currentWave;
    int currentWaveNumber;

    int remainingEnemiesToSpawn;
    float nextSpawnTime;

    int livingEnemiesCount;
	
    void Update () {
        if(remainingEnemiesToSpawn > 0 && Time.time > nextSpawnTime ){
            remainingEnemiesToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpan;
            Enemy spawnedEnemy = Instantiate(enemyPreFab, Vector3.zero, Quaternion.identity) as Enemy;
            livingEnemiesCount++;
            spawnedEnemy.onDeath += OnDeath;
        }	
        if(livingEnemiesCount == 0 && currentWaveNumber <= waves.Length){
            NextWave();
        }
	}
    private void OnDeath(){
        livingEnemiesCount--;
    }

    private void NextWave()
    {
        currentWaveNumber++;
        currentWave = waves[currentWaveNumber - 1];
        remainingEnemiesToSpawn = currentWave.enemyCount;
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpan;
    }
}
