using UnityEngine;

public class CampingFeature: MonoBehaviour
{
    float timeBetweenCampingChecks = 2;
    float campThresholdDistance = 1.5f;
    float nextCampCheckTime;
    Vector3 campPositionOld;
    public bool isCamping { get; private set; }

    public void checkCamping (Player player) {
        if ( Time.time > nextCampCheckTime)
        {
            nextCampCheckTime = Time.time + timeBetweenCampingChecks;
            isCamping = (Vector3.Distance(player.transform.position, campPositionOld) < campThresholdDistance);
            campPositionOld = player.transform.position;
        }
	}
}
