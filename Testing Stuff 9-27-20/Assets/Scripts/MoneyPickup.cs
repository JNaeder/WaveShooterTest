using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    public float moneyAmount;
    public float getRadius;
    public float moveSpeed;
    public LayerMask playerLayer;


    GameManager gm;
    GuyController gc;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        gc = FindObjectOfType<GuyController>();
    }

    private void Update()
    {
        if (PlayerIsNearby()) {
            MovePickUpTowardPlayer();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gm.money += moneyAmount;
            Destroy(gameObject);
        }

    }

    bool PlayerIsNearby() {
        bool isNearby = Physics2D.OverlapCircle(transform.position, getRadius, playerLayer);
        return isNearby;
    }


    void MovePickUpTowardPlayer() {
        Vector2 dist = gc.transform.position - transform.position;
        dist.Normalize();
        transform.position += new Vector3(dist.x, dist.y, 0) * moveSpeed * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, getRadius);

    }



}
