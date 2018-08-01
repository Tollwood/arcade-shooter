using System.Collections.Generic;

public class Map
{
    public MapConfig config { get; private set; }

    public List<Coord> allCoords { get; private set; }
    private List<Coord> freeCoords;
    private Queue<Coord> shuffeledFreeCoords;
    private Queue<Coord> shuffeledCoords;

    public Map(MapConfig _config)
    {
        config = _config;
        allCoords = new List<Coord>();
        freeCoords = new List<Coord>();
        init();
    }

    private void init(){
        for (int x = 0; x < config.mapSize.x; x++)
        {
            for (int y = 0; y < config.mapSize.y; y++)
            {
                allCoords.Add(new Coord(x, y));
            }
        }
        shuffeledCoords = new Queue<Coord>(Utility.shuffleArray<Coord>(allCoords.ToArray(), config.seed));
        freeCoords = allCoords;
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffeledCoords.Dequeue();
        shuffeledCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public void shuffleFreeCoords()
    {
        shuffeledFreeCoords = new Queue<Coord>(Utility.shuffleArray<Coord>(freeCoords.ToArray(), config.seed));
    }

    public Coord GetFreeCoord()
    {
        Coord freeCoord = shuffeledFreeCoords.Dequeue();
        shuffeledFreeCoords.Enqueue(freeCoord);
        return freeCoord;
    }

    public void markCoordAsOccupiedByObstacle(List<Coord> coordToRemove){
        freeCoords.RemoveAll((Coord obj) => coordToRemove.Contains(obj));    
    }
}