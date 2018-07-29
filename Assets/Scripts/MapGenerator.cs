using UnityEngine;
using System.Collections.Generic;
using System;

public class MapGenerator : MonoBehaviour {

    public Transform tilePrefab;
    public Transform obstaclePrefab;
    public Transform navMeshMaskPrefab;
    public Transform navMeshFloor;

    public Coord maxMapSize = new Coord(25, 25);
    public float outlinePercent;
    public float tileSize = 1;

    public Map currentMap;

    public Map[] maps;
    public int mapIndex;

    List<Coord> allTileCords;
    Queue<Coord> shuffeledTileCoords;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap(){
        currentMap = maps[mapIndex];
        allTileCords = new List<Coord>();
        GetComponent<BoxCollider>().size = new Vector3(currentMap.mapSize.x* tileSize , 0.05f, currentMap.mapSize.y * tileSize);
        for (int x = 0; x < currentMap.mapSize.x; x++)
        {
            for (int y = 0; y < currentMap.mapSize.y; y++)
            {
                allTileCords.Add(new Coord(x, y));
            }
        }
        shuffeledTileCoords = new Queue<Coord>(Utility.shuffleArray<Coord>(allTileCords.ToArray(),currentMap.seed));

        string holderName = "Generated Map";

        if(transform.Find(holderName)){
            DestroyImmediate(transform.Find(holderName).gameObject);    
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = this.transform;
        foreach(Coord coord in allTileCords){
            Transform newTile = Instantiate(tilePrefab, CoordToPosition(coord), Quaternion.Euler(Vector3.right * 90)) as Transform;
            newTile.localScale = Vector3.one * (1 - outlinePercent) * tileSize;
            newTile.parent = mapHolder;
        }

        placeObstacles(mapHolder);
        buildNavMeshMask(mapHolder);
        navMeshFloor.localScale = new Vector3(maxMapSize.x, maxMapSize.y) * tileSize;
    }

    private void buildNavMeshMask(Transform mapHolder)
    {
        Vector3 positionLeft = Vector3.left * (currentMap.mapSize.x + maxMapSize.x) / 4f * tileSize + new Vector3(0.5f, 0, 0.5f);
        Transform maskLeft = Instantiate(navMeshMaskPrefab,positionLeft, Quaternion.identity) as Transform;
        maskLeft.parent = mapHolder;
        maskLeft.localScale = new Vector3((maxMapSize.x - currentMap.mapSize.x) / 2f, 1, currentMap.mapSize.y) * tileSize;

        Vector3 positionRight = Vector3.right * (currentMap.mapSize.x + maxMapSize.x) / 4f * tileSize + new Vector3(0.5f, 0, 0.5f);

        Transform maskRight = Instantiate(navMeshMaskPrefab, positionRight, Quaternion.identity) as Transform;
        maskRight.parent = mapHolder;
        maskRight.localScale = new Vector3((maxMapSize.x - currentMap.mapSize.x) / 2f, 1, currentMap.mapSize.y) * tileSize;

        Vector3 positionTop = Vector3.forward * (currentMap.mapSize.y + maxMapSize.y) / 4f * tileSize + new Vector3(0.5f, 0, 0.5f);
        Transform maskTop = Instantiate(navMeshMaskPrefab, positionTop, Quaternion.identity) as Transform;
        maskTop.parent = mapHolder;
        maskTop.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y) / 2f) * tileSize;

        Vector3 positionBottom = Vector3.back * (currentMap.mapSize.y + maxMapSize.y) / 4f * tileSize + new Vector3(0.5f, 0, 0.5f);
        Transform maskBottom = Instantiate(navMeshMaskPrefab, positionBottom, Quaternion.identity) as Transform;
        maskBottom.parent = mapHolder;
        maskBottom.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y)/2f) * tileSize;

    }

    public Vector3 CoordToPosition(Coord coord){
        return new Vector3(-currentMap.mapSize.x / 2 + 0.5f + coord.x, 0, -currentMap.mapSize.y / 2 + 0.5f+ coord.y) * tileSize;
    }

    public Coord GetRandomCoord(){
        Coord randomCoord = shuffeledTileCoords.Dequeue();
        shuffeledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }


    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
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

    private void placeObstacles(Transform mapHolder)
    {
        System.Random prng = new System.Random(currentMap.seed);
        bool[,] obstacleMap = new bool[(int)currentMap.mapSize.x, (int)currentMap.mapSize.y];

        int obstacleCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y * currentMap.obstaclePercent);
        int currentObstacleCount = 0;

        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;

            if (randomCoord != currentMap.mapCenter && MapIsFullyAccessible(obstacleMap, currentObstacleCount))
            {
                float obstacleHeight = Mathf.Lerp(currentMap.miniumObstacleHight, currentMap.maximunObstacleHight, (float) prng.NextDouble());
                Vector3 obstaclePosition = CoordToPosition(randomCoord);
                Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * obstacleHeight / 2, Quaternion.identity) as Transform;
                newObstacle.parent = mapHolder;
                newObstacle.localScale = new Vector3((1 - outlinePercent) * tileSize, obstacleHeight,(1 - outlinePercent) * tileSize)  ;

                Renderer obstacleRenderer = newObstacle.GetComponent<Renderer>();
                Material obstacleMaterial = new Material(obstacleRenderer.sharedMaterial);

                float colorPercent = randomCoord.y / (float) currentMap.mapSize.y;
                obstacleMaterial.color = Color.Lerp(currentMap.foregroundColor, currentMap.backgroundColor, colorPercent);
                                                
                obstacleRenderer.sharedMaterial = obstacleMaterial;
            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }
        }
    }
    [System.Serializable]
    public struct Coord {
        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        public int x;
        public int y;

        public override bool Equals(object obj)
        {
            if (!(obj is Coord))
                return false;

            Coord coordToEqual = (Coord)obj;
            return this.x == coordToEqual.x && this.y == coordToEqual.y;

        }

        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }
    }

    [System.Serializable]
    public class Map {
        public Coord mapSize;
        [Range(0,1)]
        public float obstaclePercent;
        public int seed;

        public float miniumObstacleHight;
        public float maximunObstacleHight;

        public Color foregroundColor;
        public Color backgroundColor;

        public Coord mapCenter {
            get {
                return new Coord(mapSize.x / 2, mapSize.y / 2);
            }
        }

    }
}
