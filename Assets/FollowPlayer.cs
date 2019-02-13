using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<Instantiator>().OnNewPlayer += (Player player) =>
        {
            Camera.main.transform.parent = player.transform;
            Camera.main.transform.localPosition = new Vector3(x: 0, y: 1, z: 0);
            player.onDeath += () => { Camera.main.transform.parent = null; };
        };
    }
}
