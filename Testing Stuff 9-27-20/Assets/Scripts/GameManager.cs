using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int enemiesKilled;
    public int enemiesSpawned;
    public float money;


    public enum GameState {onWave, offWave, buying}
    public GameState currentGameState;


    WaveManager wm;

    // Start is called before the first frame update
    void Start()
    {
        wm = GetComponent<WaveManager>();

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

    public void StartWave() {
        currentGameState = GameState.onWave;
        wm.StartSpawning();

    }



    
}
