using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof (ObstaclePlacer))]
public class MapGenerator : MonoBehaviour {

    public Coord maxMapSize = new Coord(25, 25);
    public Transform tilePrefab;
    public Transform navMeshMaskPrefab;
    public Transform navMeshFloor;
    public float outlinePercent;
    public float tileSize = 1;

    public int mapIndex;
    public MapConfig[] maps;
    public Map generatedMap;
    Transform mapHolder;
   
    ObstaclePlacer obstacleFactory;

    private void OnEnable()
    {
        Spawner spwawner =  FindObjectOfType<Spawner>();
        obstacleFactory = GetComponent<ObstaclePlacer>();
        spwawner.OnNewWave += OnNewWave; 
    }

    private void OnNewWave(int currentWaveNumber)
    {
        mapIndex = currentWaveNumber - 1;
        GenerateMap();
    }

    public void GenerateMap(){
        Map map = newMap(maps[mapIndex]);
        buildNavMeshMask(map.config);
        placeTiles(map);
        obstacleFactory.placeObstacles(map, mapHolder,outlinePercent,tileSize);
        map.shuffleFreeTiles();
        generatedMap = map;
    }

    private void placeTiles(Map map)
    {
        foreach (Coord coord in map.GetAllTilesCords())
        {
            Transform newTile = Instantiate(tilePrefab, map.CoordToPosition(coord), Quaternion.Euler(Vector3.right * 90)) as Transform;
            newTile.localScale = Vector3.one * (1 - outlinePercent) * tileSize;
            newTile.parent = mapHolder;
            map.AddTileAt(coord, newTile);
        }
    }


    private Map newMap(MapConfig mapConfig){
        string holderName = "Generated Map";

        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = this.transform;
        return new Map(mapConfig, tileSize);
    }

    private void buildNavMeshMask(MapConfig currentMap)
    {
        Vector3 offSet = new Vector3(withUnEvanOffset(currentMap.mapSize.x), 0, withUnEvanOffset(currentMap.mapSize.y)) * tileSize;
        GetComponent<BoxCollider>().size = new Vector3(currentMap.mapSize.x * tileSize, 0.05f, currentMap.mapSize.y * tileSize);
        GetComponent<BoxCollider>().center = offSet;

        navMeshFloor.localScale = new Vector3(maxMapSize.x, maxMapSize.y) * tileSize;
        navMeshFloor.position = offSet;
        
        Vector3 positionLeft = Vector3.left * (currentMap.mapSize.x + maxMapSize.x) / 4f * tileSize + offSet;
        Transform maskLeft = Instantiate(navMeshMaskPrefab,positionLeft, Quaternion.identity) as Transform;
        maskLeft.parent = mapHolder;
        maskLeft.localScale = new Vector3((maxMapSize.x - currentMap.mapSize.x) / 2f, 1, currentMap.mapSize.y) * tileSize;

        Vector3 positionRight = Vector3.right * (currentMap.mapSize.x + maxMapSize.x) / 4f * tileSize + offSet;
        Transform maskRight = Instantiate(navMeshMaskPrefab, positionRight, Quaternion.identity) as Transform;
        maskRight.parent = mapHolder;
        maskRight.localScale = new Vector3((maxMapSize.x - currentMap.mapSize.x) / 2f, 1, currentMap.mapSize.y) * tileSize;

        Vector3 positionTop = Vector3.forward * (currentMap.mapSize.y + maxMapSize.y) / 4f * tileSize + offSet;
        Transform maskTop = Instantiate(navMeshMaskPrefab, positionTop, Quaternion.identity) as Transform;
        maskTop.parent = mapHolder;
        maskTop.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y) / 2f) * tileSize;

        Vector3 positionBottom = Vector3.back * (currentMap.mapSize.y + maxMapSize.y) / 4f * tileSize +offSet;
        Transform maskBottom = Instantiate(navMeshMaskPrefab, positionBottom, Quaternion.identity) as Transform;
        maskBottom.parent = mapHolder;
        maskBottom.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y)/2f) * tileSize;

    }

    private float withUnEvanOffset(int value)
    {
        if (value % 2 == 1)
        {
            return 0.5f;
        }
        return 0;
    }

}
