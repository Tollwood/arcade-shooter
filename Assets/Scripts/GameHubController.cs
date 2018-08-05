using System.Collections.Generic;
using UnityEngine;

public class GameHubController : MonoBehaviour {

    [SerializeField]
    public List<GameObject> controlls;

    Game gameManager;

	// Use this for initialization
	void Start () {

        gameManager = FindObjectOfType<Game>();
        gameManager.OnNewGame += Enable;
        gameManager.OnGameOver += Disable;
	}
	
    private void Enable(){
        foreach(GameObject go in controlls){
            go.SetActive(true);
        }
    }
	
    private void Disable()
    {
        foreach (GameObject go in controlls)
        {
            go.SetActive(false);
        }
    }
}
