using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CampingFeature))]
[RequireComponent(typeof(SpawnTimeFeature))]
public class Spawner : MonoBehaviour
{
    

    private MapGenerator map;
    float nextSpawnTime;

    CampingFeature campingFeature;
    SpawnTimeFeature spawnTimeFeature;
    Game gm;

    Instantiator instantiator;
    Player player;

    private void Start()
    {
        map = FindObjectOfType<MapGenerator>();
        gm = FindObjectOfType<Game>();
        instantiator = FindObjectOfType<Instantiator>();
        instantiator.OnNewPlayer += OnNewPlayer;
        campingFeature = GetComponent<CampingFeature>();
        spawnTimeFeature = GetComponent<SpawnTimeFeature>();
    }

    void Update () {
        if(gm.playing && Time.time > nextSpawnTime ){
            nextSpawnTime = spawnTimeFeature.NextSpawnTime();
            campingFeature.checkCamping();
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
            spawnTile = map.GetTileFromPosition(player.transform.position);
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

        Enemy spawnedEnemy = instantiator.InstantiateEnemy(spawnTile.position + Vector3.up);
    }

    private void OnNewPlayer(Player newPlayer)
    {
        player = newPlayer;
    }
}
