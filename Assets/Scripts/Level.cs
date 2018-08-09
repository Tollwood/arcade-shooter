using UnityEngine;

[System.Serializable]
public class Level
{
    public int levelNumber;
    public float spawnTime;
    public float timeBetweenSpan;
    public float timeIncrement;

    public Coord mapSize = new Coord(0,0);
    [Range(0, 1)]
    public float obstaclePercent;
    public int seed;

    public float miniumObstacleHight;
    public float maximunObstacleHight;

    public Color foregroundColor;
    public Color backgroundColor;

    public Coord mapCenter {
        get{
            return new Coord(mapSize.x / 2, mapSize.y / 2);
        }
    }
}