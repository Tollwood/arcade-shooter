using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CampingFeature))]
[RequireComponent(typeof(SpawnTimeFeature))]
public class Spawner : MonoBehaviour
{

    public Spawnable spawnable;
    CampingFeature campingFeature;


    private SpawnTimeFeature spawnTimeFeature;
    private float nextSpawnTime;
    private Game gm;
    private Instantiator instantiator;

    private void Start()
    {
        gm = FindObjectOfType<Game>();
        instantiator = FindObjectOfType<Instantiator>();
        campingFeature = GetComponent<CampingFeature>();
        spawnTimeFeature = GetComponent<SpawnTimeFeature>();
    }

    void Update () {
        if(gm.playing && Time.time > nextSpawnTime ){
            nextSpawnTime = spawnTimeFeature.NextSpawnTime();
            StartCoroutine(SpawnEnemy());
        }
	}

    IEnumerator SpawnEnemy(){
        
        Transform spawnTile = campingFeature.GetSpawnTile();
        Material tileMat = spawnTile.GetComponent<Renderer>().material;
        Color initialColour = tileMat.color;

        float spawnTimer = 0;
        while (spawnTimer < spawnTimeFeature.spawnDelay)
        {
            tileMat.color = Color.Lerp(initialColour, spawnTimeFeature.flashColour, Mathf.PingPong(spawnTimer * spawnTimeFeature.tileFlashSpeed, 1));
            spawnTimer += Time.deltaTime;
            yield return null;
        }

        instantiator.Instantiate(spawnable ,spawnTile.position + Vector3.up);
    }

}
