using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RemainingTimeDisplayController : MonoBehaviour {

    Spawner spawner;
    TextMeshProUGUI textElement;
	void Start () {
        spawner = FindObjectOfType<Spawner>();
        textElement = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
        textElement.text = Mathf.Round(spawner.remainingTimeToSpawn)+"";
	}
}
