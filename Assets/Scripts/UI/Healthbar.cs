using UnityEngine;

public class Healthbar : MonoBehaviour {

    public GameObject leftHeartPrefab;
    public GameObject rightHeartPrefab;
    public float width = 10;
    public float height = 20;

    private Player player = null;
    private int renderedHealth = 0;
	
	void Update () {

        if(player == null){
            player = FindObjectOfType<Player>();
        }

        if(player != null && renderedHealth != player.health){
            removeChildren();
            renderHealth(player.health);
            renderedHealth = player.health;
        }
	}

    private void renderHealth(int healthPoints)
    {
        for (int i = 0; i < healthPoints; i++){
            GameObject prefab = i % 2 == 0 ? leftHeartPrefab : rightHeartPrefab;
            GameObject heart = Instantiate(prefab, transform);
            RectTransform rectTransform = heart.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(width * i, -height/2 , 0);
        }
    }

    private void removeChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
