using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public Map(MapConfig _config, float _tileSize){
        config = _config;
        tileSize = _tileSize;
        allTileCords = new List<Coord>();
        freeTileCords = new List<Coord>();


        for (int x = 0; x < config.mapSize.x; x++)
        {
            for (int y = 0; y < config.mapSize.y; y++)
            {
                allTileCords.Add(new Coord(x, y));
            }
        }
        shuffeledTileCoords = new Queue<Coord>(Utility.shuffleArray<Coord>(allTileCords.ToArray(), config.seed));
        freeTileCords = allTileCords;

        tileMap = new Transform[config.mapSize.x, config.mapSize.y];
    }

    public MapConfig config;
    List<Coord> allTileCords;

    public List<Coord> GetAllTilesCords(){
        return allTileCords;
    }
    List<Coord> freeTileCords;
    Queue<Coord> shuffeledFreeTileCoords;
    Transform[,] tileMap;
    Queue<Coord> shuffeledTileCoords;
    float tileSize;

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffeledTileCoords.Dequeue();
        shuffeledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public Vector3 CoordToPosition(Coord coord)
    {
        return new Vector3(-config.mapSize.x / 2 + 0.5f + coord.x, 0, -config.mapSize.y / 2 + 0.5f + coord.y) * tileSize;
    }

    internal void AddTileAt(Coord coord, Transform newTile)
    {
        tileMap[coord.x, coord.y] = newTile;
    }

    public void shuffleFreeTiles()
    {
        shuffeledFreeTileCoords = new Queue<Coord>(Utility.shuffleArray<Coord>(freeTileCords.ToArray(), config.seed));
    }

    public Transform GetFreeTilePosition()
    {
        Coord freeCoord = shuffeledFreeTileCoords.Dequeue();
        shuffeledFreeTileCoords.Enqueue(freeCoord);
        return tileMap[freeCoord.x, freeCoord.y];
    }

    public Transform GetTileFromPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / tileSize + (config.mapSize.x - 1) / 2f);
        int y = Mathf.RoundToInt(position.z / tileSize + (config.mapSize.y - 1) / 2f);
        x = Mathf.Clamp(x, 0, tileMap.GetLength(0) - 1);
        y = Mathf.Clamp(y, 0, tileMap.GetLength(1) - 1);
        return tileMap[x, y];
    }

    public void removeFreeTileCoords(Coord coordToRemove){
        freeTileCords.Remove(coordToRemove);    
    }


}