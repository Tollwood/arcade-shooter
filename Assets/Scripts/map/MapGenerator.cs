using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof (ObstacleFactory))]
[RequireComponent(typeof(Navigation))]
public class MapGenerator : MonoBehaviour {

    public Coord maxMapSize = new Coord(25, 25);
    public float outlinePercent;
    public float tileSize = 1;

    public int mapIndex;
    public MapConfig[] maps;
    public Map generatedMap;

    string holderName = "Generated Map";
    Transform mapHolder;
    Transform[,] tileMap;

    ObstacleFactory obstacleFactory;
    Navigation navigation;

    private void Start()
    {
        Spawner spawner = FindObjectOfType<Spawner>();
        spawner.OnNewWave += OnNewWave;
        GenerateMap();
    }

    private void OnNewWave(int currentWaveNumber)
    {
        mapIndex = currentWaveNumber - 1;
        GenerateMap();
    }

    public void GenerateMap(){
        deleteCurrentMap();
        Map map = new Map(maps[mapIndex]);
        navigation = GetComponent<Navigation>();
        navigation.setup(map.config,tileSize,mapHolder,maxMapSize);
        obstacleFactory = GetComponent<ObstacleFactory>();
        tileMap = obstacleFactory.placeGroundTiles(map,mapHolder,outlinePercent,tileSize);
        List<Coord> CoordsWithObstacle = obstacleFactory.placeObstacles(map, mapHolder,outlinePercent,tileSize);
        map.markCoordAsOccupiedByObstacle(CoordsWithObstacle);
        map.shuffleFreeCoords();
        generatedMap = map;
    }

    private void deleteCurrentMap(){
        if (transform.Find(holderName)){
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = this.transform;
    }

    public Transform GetFreeTilePosition()
    {
        Coord freeCoord = generatedMap.GetFreeCoord();
        return tileMap[freeCoord.x, freeCoord.y];
    }

    public Transform GetTileFromPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / tileSize + (generatedMap.config.mapSize.x - 1) / 2f);
        int y = Mathf.RoundToInt(position.z / tileSize + (generatedMap.config.mapSize.y - 1) / 2f);
        x = Mathf.Clamp(x, 0, tileMap.GetLength(0) - 1);
        y = Mathf.Clamp(y, 0, tileMap.GetLength(1) - 1);
        return tileMap[x, y];
    }
}
