using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreDisplayController : MonoBehaviour {

    ScoreCountFeature scoreCountFeature;

    TextMeshProUGUI textElement;
	void Start () {
        scoreCountFeature = FindObjectOfType<ScoreCountFeature>();
        textElement = GetComponent<TextMeshProUGUI>();
	}
	

	void Update () {
        textElement.text = scoreCountFeature.score + "";
	}
}
