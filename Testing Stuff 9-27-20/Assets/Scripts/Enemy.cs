using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public float health = 10f;
    public GameObject moneyPickup;
    public float moneyDropRadius;

    AIDestinationSetter setter;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        setter = GetComponent<AIDestinationSetter>();
        setter.target = FindObjectOfType<GuyController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    void CheckHealth() {
        if (health <= 0) {
            Die();
        }

    }

    void Die() {
        gm.KillEnemy();
        DropMoney();
        Destroy(gameObject);
    }
    

    public void TakeDamage() {
        health--;
    }

    void DropMoney() {
        int randAmount = Random.Range(1, 3);

        for (int i = 0; i < randAmount; i++)
        {
            Vector3 newPos = new Vector3(Random.Range(-moneyDropRadius, moneyDropRadius), Random.Range(-moneyDropRadius, moneyDropRadius), 0);
            newPos += transform.position;
            Instantiate(moneyPickup, newPos, Quaternion.identity);

        }

    }


    
}
