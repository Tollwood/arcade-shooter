using UnityEngine;

public class CampingFeature: MonoBehaviour
{
    Instantiator instantiator;
    Player player;

    float timeBetweenCampingChecks = 2;
    float campThresholdDistance = 1.5f;
    float nextCampCheckTime;
    Vector3 campPositionOld;

    public bool isCamping { get; private set; }

    private void Start()
    {
        instantiator = FindObjectOfType<Instantiator>();
        instantiator.OnNewPlayer += onNewPlayer;
    }

    private void onNewPlayer(Player newPlayer){
        player = newPlayer;
    }

    public void checkCamping () {
        if (player !=null && Time.time > nextCampCheckTime)
        {
            nextCampCheckTime = Time.time + timeBetweenCampingChecks;
            isCamping = (Vector3.Distance(player.transform.position, campPositionOld) < campThresholdDistance);
            campPositionOld = player.transform.position;
        }
	}
}
