using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RemainingTimeDisplayController : MonoBehaviour {

    Game gm;
    TextMeshProUGUI textElement;

	void Start () {
        gm = FindObjectOfType<Game>();
        textElement = GetComponent<TextMeshProUGUI>();
	}
	
	void Update () {
        textElement.text = Mathf.Round(gm.remainingTime)+"";
	}
}
