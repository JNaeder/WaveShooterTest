using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float damageAmount;
    public enum AmmoType{Energy, Earth};
    public AmmoType bulletAmmoType;
    public bool isSteady;

    StatTracker st;

    private void Start()
    {
        if (!isSteady)
        {
            StartCoroutine(DestroyBullet());
        }
    }

    IEnumerator DestroyBullet() {
        yield return new WaitForSeconds(5f);
        StatTracker.shotsMissed++;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isSteady)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Enemy newEnemy = collision.gameObject.GetComponent<Enemy>();
                newEnemy.TakeDamage(damageAmount);
                Destroy(gameObject);
                StatTracker.shotsHit++;
            }
            else
            {
                Destroy(gameObject);
                StatTracker.shotsMissed++;
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isSteady)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(damageAmount * Time.deltaTime);

            }
        }
    }
}
