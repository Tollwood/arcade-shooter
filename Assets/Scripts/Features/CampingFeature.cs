using UnityEngine;

public class CampingFeature: MonoBehaviour
{
    
    public float timeBetweenCampingChecks = 2;
    public float campThresholdDistance = 1.5f;

    private float nextCampCheckTime;
    private Vector3 campPositionOld;
    private Player player;
    private MapGenerator map;
    private bool isCamping = false;

    private void Start()
    {
        enabled = PlayerPrefs.GetInt("CampingEnabled", 0) == 0;
        FindObjectOfType<Instantiator>().OnNewPlayer += (Player newPlayer) => { player = newPlayer; };
        map = FindObjectOfType<MapGenerator>();
    }

    private void Update()
    {
        if (enabled && player != null && Time.time > nextCampCheckTime)
        {
            nextCampCheckTime = Time.time + timeBetweenCampingChecks;
            isCamping = (Vector3.Distance(player.transform.position, campPositionOld) < campThresholdDistance);
            campPositionOld = player.transform.position;
        }
    }

    internal Transform GetSpawnTile()
    {
        return enabled && isCamping 
            ? map.GetTileFromPosition(player.transform.position)
            : map.GetFreeTilePosition();
    }
}
