using System;
using UnityEngine;

public class Game : MonoBehaviour {
    
    public Action OnGameOver;
    public Action OnNewGame;
    public Player playerPrefab;

    public void GameOver(){
        OnGameOver();
    }

    public void NewGame(){
        Player player =  Instantiate(playerPrefab, transform);
        player.onDeath += GameOver;
        OnNewGame();
    }
  
}
