using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int waveNumber;

    EnemySpawner es;
    GameManager gm;
    GuyController gc;

    public bool isInWave;
    int alreadySpawned;
    private void Awake()
    {
        gc = FindObjectOfType<GuyController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        es = FindObjectOfType<EnemySpawner>();
        gm = GetComponent<GameManager>();
    }


    private void Update()
    {


        if (isInWave)
        {
            if (gm.enemiesSpawned >= SpawnAmount() + alreadySpawned)
            {
                es.IsSpawning(false);
                isInWave = false;
                alreadySpawned = gm.enemiesSpawned;
                gm.SetGameState(GameManager.GameState.offWave);
            }
        }
    }



    public void StartSpawning() {
        waveNumber++;
        es.IsSpawning(true);
        isInWave = true;
    }

    int SpawnAmount()
    {
        int newAmount = (waveNumber * 5);
        return newAmount;
    }
    
}
