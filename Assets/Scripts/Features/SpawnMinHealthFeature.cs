﻿using System.Collections.Generic;
using UnityEngine;

public class SpawnMinHealthFeature : ISpawnFeature
{
    public int minHealth = 5;
    public int maxHealthBoosts = 3;
    public float timeBetweenChecks = 2;

    LivingEntity player;
    Game game;
    int currentHealthBoostCount = 0;

    private float nextCheckTime;
    private bool spawning = false;
    private List<GameObject> gos;
    public override bool ShouldSpawn()
    {
        if(player == null ){
            player = FindObjectOfType<Player>();
        }
        if(player == null){
            return false;
        }

        if(Time.time > nextCheckTime && !spawning ){
            nextCheckTime = Time.time + timeBetweenChecks;
            spawning = player.health <= minHealth && gos.FindAll((GameObject obj) => obj != null).Count <= maxHealthBoosts;
            return spawning;
        }
        return false;
    }

    public override void Spawned(GameObject spawnedGo)
    {
        gos.Add(spawnedGo);
        spawning = false;
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        game = FindObjectOfType<Game>();
        game.OnNewLevel += resetList;

    }

    private void resetList(Level level)
    {
        gos = new List<GameObject>();
    }
}