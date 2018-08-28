using UnityEngine;

public class SpawnTimeFeature : MonoBehaviour
{
    public float currentTimeBetweenSpan;
    public float timeIncrement;

    public float spawnDelay = 1;
    public float tileFlashSpeed = 4;
    public Color flashColour = Color.red;

    void Start()
    {
        FindObjectOfType<Game>().OnNewLevel += OnNewLevel;
        FindObjectOfType<Instantiator>().OnNewEnemy += OnNewEnemy;
    }

    public float NextSpawnTime(){
        return Time.time + currentTimeBetweenSpan;
    }

    public void reduceSpawnSpeed(float timeIncrease)
    {
        currentTimeBetweenSpan += timeIncrease;
    }

    void OnNewEnemy(Enemy enemy){
        currentTimeBetweenSpan -= timeIncrement;
    }

    void OnNewLevel(Level newLevel){
        currentTimeBetweenSpan = newLevel.timeBetweenSpan;
        timeIncrement = newLevel.timeIncrement;
    }
}
