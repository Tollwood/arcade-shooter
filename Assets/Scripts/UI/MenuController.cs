using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public Image backGround;

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
        Color tmpColor = backGround.color;
        tmpColor.a = 150;
        backGround.color = tmpColor;
    }

    private void OnNewGame()
    {
        Color tmpColor = backGround.color;
        tmpColor.a = 0;
        backGround.color = tmpColor;
        mainMenu.SetActive(false);
    }
}
