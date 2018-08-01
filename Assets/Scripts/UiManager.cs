using System;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {


    public Image gameOverBG;
    public GameObject gameOverMenu;

    private void Start (){
        Game gm = FindObjectOfType<Game>();
        gm.OnGameOver += OnGameOver;
        gm.OnNewGame += OnNewGame;
    }

    private void OnGameOver()
    {
        Color tmpColor = gameOverBG.color;
        tmpColor.a = 150;
        gameOverBG.color = tmpColor;
        gameOverMenu.SetActive(true);
    }

    private void OnNewGame(){
        Color tmpColor = gameOverBG.color;
        tmpColor.a = 0;
        gameOverBG.color = tmpColor;
        gameOverMenu.SetActive(false);
    }
}
