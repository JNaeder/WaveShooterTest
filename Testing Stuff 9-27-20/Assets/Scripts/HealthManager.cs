using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    GameManager gm;

    public float startHealth = 3f;
    public float startShields = 10f;

    public float invincibleTime = 5f;
    public bool isInvincible;
    public Color invincibleColor;

    public SpriteRenderer sp;
    Color spriteStartColor;



    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        spriteStartColor = sp.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        gm.health = startHealth;
        gm.shields = startShields;

    }

    public void TakeDamage(float newDamage) {
        Debug.Log("Take " + newDamage + "Damage");

        if (gm.shields > 0)
        {
            gm.shields -= newDamage;
        }
        else
        {
            
            gm.health -= newDamage;
            if (gm.health <= 0) {
                Die();
            }
        }
        StartCoroutine(SetInvincible());
    }

    IEnumerator SetInvincible()
    {
        isInvincible = true;
        SetSpriteColor(invincibleColor);
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
        SetSpriteColor(spriteStartColor);
    }

    public void RechargeShields(float shieldAmount) {
        gm.shields += shieldAmount;
    }

    void SetSpriteColor(Color newColor) {
        sp.color = newColor;
    }

    void Die() {
        Debug.Log("You have died");
    }
    

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy"){
            Enemy newEnemy = collision.gameObject.GetComponent<Enemy>();
            if (!isInvincible)
            {
                TakeDamage(newEnemy.attackDamage);
                
            }
        }
    }


}
