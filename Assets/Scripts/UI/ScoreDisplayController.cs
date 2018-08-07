using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreDisplayController : MonoBehaviour {

    ScoreCounter scoreCounter;
    TextMeshProUGUI textElement;
	void Start () {
        scoreCounter = FindObjectOfType<ScoreCounter>();
        textElement = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
        textElement.text = scoreCounter.score + "";
	}
}
