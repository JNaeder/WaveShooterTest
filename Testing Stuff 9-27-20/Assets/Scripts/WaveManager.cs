using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int waveNumber;

    EnemySpawner es;
    GameManager gm;

    public bool isInWave;
    int alreadySpawned;
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
            }
        }
    }



    public void StartSpawning() {
        waveNumber++;
        es.IsSpawning(true);
        isInWave = true;
        Debug.Log("Start Wave");
    }

    int SpawnAmount()
    {
        int newAmount = (waveNumber * 5);
        return newAmount;
    }
    
}
