using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnFrequency = 1f;

    Transform[] spawnLocations;

    GameManager gm;

    float newTime;
    public  bool isSpawning;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        spawnLocations = GetComponentsInChildren<Transform>();

        newTime = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isSpawning) {
            Spawning();
        }
    }

    void Spawning() {
        if (Time.time > newTime + spawnFrequency)
        {
            SpawnEnemy();
            newTime = Time.time;

        }

    }


    Vector3 RandomSpawnLocation() {
        int randNum = Random.Range(1, spawnLocations.Length);
        Transform spawnTrans = spawnLocations[randNum];
        return spawnTrans.position;
    }

    void SpawnEnemy() {
        Instantiate(enemyPrefab, RandomSpawnLocation(), Quaternion.identity);
        gm.SpawnEnemy();
    }


    public void IsSpawning(bool newBool) {
        isSpawning = newBool;
    }

    public void SetSpawnFrequency(float newFreq) {
        spawnFrequency = newFreq;

    }
}
