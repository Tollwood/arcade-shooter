using System.Collections;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(CampingFeature))]
public abstract class Spawner : MonoBehaviour
{

    public Spawnable spawnable;
    public float spawnDelay = 1;
    public float tileFlashSpeed = 4;
    public Color flashColour = Color.red;

    public abstract void Spawned(GameObject spawnedGo);
    public abstract bool ShouldSpawn();
    CampingFeature campingFeature;

    private Game gm;
    private Instantiator instantiator;

    private void Start()
    {
        gm = FindObjectOfType<Game>();
        instantiator = FindObjectOfType<Instantiator>();
        campingFeature = GetComponent<CampingFeature>();
    }

    void Update () {
        if(gm == null){
            gm = FindObjectOfType<Game>();
        }
        if(gm != null && gm.playing && ShouldSpawn() ){
            StartCoroutine(SpawnEnemy());
        }
	}

    IEnumerator SpawnEnemy(){

        if(campingFeature == null ){
            campingFeature = GetComponent<CampingFeature>();
        }
        Transform spawnTile = campingFeature.GetSpawnTile();
        Material tileMat = spawnTile.GetComponent<Renderer>().material;
        Color initialColour = tileMat.color;

        float spawnTimer = 0;
        while (spawnTimer < spawnDelay)
        {
            tileMat.color = Color.Lerp(initialColour, flashColour, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1));
            spawnTimer += Time.deltaTime;
            yield return null;
        }
        if(instantiator == null){
            instantiator = FindObjectOfType<Instantiator>();
        }
        GameObject instance = instantiator.Instantiate(spawnable ,spawnTile.position + Vector3.up);
        Spawned(instance);
    }

}
