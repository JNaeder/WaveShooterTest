using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    StatTracker st;

    private void Start()
    {
        

        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet() {
        yield return new WaitForSeconds(5f);
        StatTracker.shotsMissed++;
        Destroy(gameObject);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy newEnemy = collision.gameObject.GetComponent<Enemy>();
            newEnemy.TakeDamage();
            Destroy(gameObject);
            StatTracker.shotsHit++;
        }
        else {
            Destroy(gameObject);
            StatTracker.shotsMissed++;
        }

        
    }


}
