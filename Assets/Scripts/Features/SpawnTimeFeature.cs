using UnityEngine;

public class SpawnTimeFeature : Spawner
{
    public float currentTimeBetweenSpan;
    public float timeIncrement;

    private float nextSpawnTime;
    private bool nextSpawn = false;

    void Start()
    {
        FindObjectOfType<Game>().OnNewLevel += OnNewLevel;
        FindObjectOfType<Instantiator>().OnNewEnemy += OnNewEnemy;
        nextSpawnTime = Time.time + currentTimeBetweenSpan;
    }


    public void reduceSpawnSpeed(float timeIncrease)
    {
        currentTimeBetweenSpan += timeIncrease;
    }

    void OnNewEnemy(Enemy enemy)
    {
        currentTimeBetweenSpan -= timeIncrement;
    }

    void OnNewLevel(Level newLevel)
    {
        currentTimeBetweenSpan = newLevel.timeBetweenSpan;
        timeIncrement = newLevel.timeIncrement;
    }

    public override bool ShouldSpawn()
    {
        if(!nextSpawn){
            nextSpawn = Time.time > nextSpawnTime;
            if(nextSpawn){
                nextSpawnTime = Time.time + currentTimeBetweenSpan;        
                nextSpawn = false;
                return true;
            }
        }
        return nextSpawn;
    }

    public override void Spawned(GameObject spawnedGo)
    {
    }

}
