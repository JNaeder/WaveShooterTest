using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public float health = 10f;

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
        Destroy(gameObject);
    }
    

    public void TakeDamage() {
        health--;
    }


    
}
