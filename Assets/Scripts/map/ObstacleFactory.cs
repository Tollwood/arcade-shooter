using System.Collections.Generic;
using UnityEngine;


public class ObstacleFactory: MonoBehaviour {

    public Transform obstaclePrefab;
    public Transform tilePrefab;

    public Transform[,] placeGroundTiles(Map map, Transform mapHolder, float outlinePercent, float tileSize)
    {
        Transform[,] tileMap = new Transform[map.config.mapSize.x, map.config.mapSize.y];
        foreach (Coord coord in map.allCoords)
        {
            Transform newTile = Instantiate(tilePrefab, CoordToPosition(map.config.mapSize, coord, tileSize), Quaternion.Euler(Vector3.right * 90)) as Transform;
            newTile.localScale = Vector3.one * (1 - outlinePercent) * tileSize;
            newTile.parent = mapHolder;
            tileMap[coord.x, coord.y] = newTile;
        }
        return tileMap;
    }

    public Vector3 CoordToPosition(Coord mapSize, Coord coord, float tileSize)
    {
        return new Vector3(-mapSize.x / 2 + 0.5f + coord.x, 0, -mapSize.y / 2 + 0.5f + coord.y) * tileSize;
    }


    public List<Coord> placeObstacles(Map map, Transform mapHolder,float outlinePercent, float tileSize)
    {
        List<Coord> tilesWithObstacle = new List<Coord>();
        System.Random prng = new System.Random(map.config.seed);
        bool[,] obstacleMap = new bool[(int)map.config.mapSize.x, (int)map.config.mapSize.y];

        int obstacleCount = (int)(map.config.mapSize.x * map.config.mapSize.y * map.config.obstaclePercent);
        int currentObstacleCount = 0;

        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = map.GetRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;

            if (randomCoord != map.config.mapCenter && MapIsFullyAccessible(map.config, obstacleMap, currentObstacleCount))
            {
                float obstacleHeight = Mathf.Lerp(map.config.miniumObstacleHight, map.config.maximunObstacleHight, (float)prng.NextDouble());
                Vector3 obstaclePosition = CoordToPosition(map.config.mapSize, randomCoord,tileSize);
                Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * obstacleHeight / 2, Quaternion.identity) as Transform;
                newObstacle.parent = mapHolder;
                newObstacle.localScale = new Vector3((1 - outlinePercent) * tileSize, obstacleHeight, (1 - outlinePercent) * tileSize);

                Renderer obstacleRenderer = newObstacle.GetComponent<Renderer>();
                Material obstacleMaterial = new Material(obstacleRenderer.sharedMaterial);

                float colorPercent = randomCoord.y / (float)map.config.mapSize.y;
                obstacleMaterial.color = Color.Lerp(map.config.foregroundColor, map.config.backgroundColor, colorPercent);

                obstacleRenderer.sharedMaterial = obstacleMaterial;
                tilesWithObstacle.Add(randomCoord);
            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }
        }
        return tilesWithObstacle;
    }

    bool MapIsFullyAccessible(MapConfig currentMap, bool[,] obstacleMap, int currentObstacleCount)
    {
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(currentMap.mapCenter);
        mapFlags[currentMap.mapCenter.x, currentMap.mapCenter.y] = true;

        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if (x == 0 || y == 0)
                    {
                        if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlags[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }
        }

        int targetAccessibleTileCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y - currentObstacleCount);
        return targetAccessibleTileCount == accessibleTileCount;
    }
}