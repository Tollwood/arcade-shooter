using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject backGround;

    private void Start()
    {
        Game gm = FindObjectOfType<Game>();
        gm.OnGameOver += OnGameOver;
        gm.OnNewGame += OnNewGame;
    }

    public void onShowSettings(){
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void onShowMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    private void OnGameOver(){
        mainMenu.SetActive(true);
        backGround.SetActive(true);
    }

    private void OnNewGame()
    {
        backGround.SetActive(false);
        mainMenu.SetActive(false);
    }
}
