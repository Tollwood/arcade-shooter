using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SpawnIntervallDisplayController : MonoBehaviour {

    SpawnTimeFeature spawnTimeFeature;
    public TextMeshProUGUI textElement;
    public Image image;
	void Start () {
        GameObject enemySpawner = GameObject.FindWithTag("EnemySpawner");
        if(enemySpawner != null){
            spawnTimeFeature = enemySpawner.GetComponent<SpawnTimeFeature>();    
        }
        else {
            textElement.enabled = false;
            image.enabled = false;    
        }
	}
	
	void Update () {
        if(spawnTimeFeature != null){
            textElement.text = spawnTimeFeature.currentTimeBetweenSpan.ToString("F2");    
        }
	}
}
