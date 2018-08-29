using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CampingFeature))]
[RequireComponent(typeof(ISpawnFeature))]
public class Spawner : MonoBehaviour
{

    public Spawnable spawnable;
    CampingFeature campingFeature;


    private ISpawnFeature spawnFeature;
    private Game gm;
    private Instantiator instantiator;

    private void Start()
    {
        gm = FindObjectOfType<Game>();
        instantiator = FindObjectOfType<Instantiator>();
        campingFeature = GetComponent<CampingFeature>();
        spawnFeature = GetComponent<ISpawnFeature>();
    }

    void Update () {
        if(gm.playing && spawnFeature.ShouldSpawn() ){
            StartCoroutine(SpawnEnemy());
        }
	}

    IEnumerator SpawnEnemy(){
        
        Transform spawnTile = campingFeature.GetSpawnTile();
        Material tileMat = spawnTile.GetComponent<Renderer>().material;
        Color initialColour = tileMat.color;

        float spawnTimer = 0;
        while (spawnTimer < spawnFeature.spawnDelay)
        {
            tileMat.color = Color.Lerp(initialColour, spawnFeature.flashColour, Mathf.PingPong(spawnTimer * spawnFeature.tileFlashSpeed, 1));
            spawnTimer += Time.deltaTime;
            yield return null;
        }

        GameObject instance = instantiator.Instantiate(spawnable ,spawnTile.position + Vector3.up);
        spawnFeature.Spawned(instance);
    }

}
