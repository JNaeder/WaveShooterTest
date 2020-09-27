using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int enemiesKilled;
    public int enemiesSpawned;

    public enum GameState {onWave, offWave, buying}
    public GameState currentGameState;

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.offWave;
    }
    


    public void KillEnemy() {
        enemiesKilled++;
    }

    public void SpawnEnemy() {
        enemiesSpawned++;
    }


    public void SetGameState(GameState newState) {
        currentGameState = newState;
    }



    
}
