using System.Collections.Generic;
using UnityEngine;

public class GameHubController : MonoBehaviour {

    [SerializeField]
    public List<GameObject> controlls;

	void Start () {
        Game gameManager= FindObjectOfType<Game>();
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
