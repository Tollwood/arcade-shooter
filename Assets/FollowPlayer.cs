using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    GameObject player;

	void LateUpdate () {
	
        if(player == null){
            player = GameObject.FindWithTag("Player");
        }
        if(player != null){
            Camera.main.transform.position = player.transform.position + (Vector3.up * 30);
         }
	}
}
