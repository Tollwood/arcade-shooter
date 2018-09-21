using System.Collections.Generic;
using UnityEngine;

public class SpawnMinRemainingTimeFeature : Spawner
{
    public int minRemainingTime = 15;
    public int maxItems = 3;
    public int maxTotal = 10;
    public float timeBetweenChecks = 0.5f;
    public float timeBetweenSpwans = 5;

    Game game;
    private float nextCheckTime;
    private float earliestNextSpawn;
    private bool spawning = false;
    private List<GameObject> gos;
    private int currentTotal = 0;


    public override bool ShouldSpawn()
    {
        if(Time.time > nextCheckTime && !spawning && Time.time > earliestNextSpawn ){
            nextCheckTime = Time.time + timeBetweenChecks;
            spawning = game.remainingTime <= minRemainingTime && gos.FindAll((GameObject obj) => obj != null).Count < maxItems &&  currentTotal <= maxTotal;
            return spawning;
        }
        return false;
    }

    public override void Spawned(GameObject spawnedGo)
    {
        gos.Add(spawnedGo);
        earliestNextSpawn = Time.time + timeBetweenSpwans;
        spawning = false;
        currentTotal++;
    }

    void Start()
    {
        game = FindObjectOfType<Game>();
        game.OnNewLevel += resetList;
    }

    private void resetList(Level level){
        gos = new List<GameObject>();
        currentTotal = 0;
    }

}
