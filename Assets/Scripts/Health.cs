using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health, maxHealth;
    public GameObject enemyObject;

    private void Awake()
    {
        //Generates the health of each enemy and character
        if(name != "Character")
        {
            float time = Score.secondsCount;
            //Increases max health of newly generated enemies over time
            if (time <= 60)
            {
                maxHealth = 50;
            }
            else if (time > 60 && time <= 120)
            {
                maxHealth = 100;
            }
            else if (time > 120 && time <= 240)
            {
                maxHealth = 150;
            }
            else
            {
                maxHealth = 200;
            }
            
        }
        else
        {

            maxHealth = 300;
        }
        
        health = maxHealth;
    }

    bool killed;

    private void Update()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, GetComponent<Health>().health / GetComponent<Health>().maxHealth, GetComponent<Health>().health / GetComponent<Health>().maxHealth);
        if (health < 0)
        {
            if(CompareTag("Enemy") && !killed)
            {
                killed = true;
                Score.kills += 1;
                StartCoroutine(EnemyDeath());
            }
        }
    }

    public GameObject deathParticles;

    public IEnumerator EnemyDeath()
    {
        deathParticles.SetActive(true);
        enemyObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.15f);
        Destroy(enemyObject);
    }    

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if ((collider.gameObject.CompareTag("Enemy Bullet") && name == "Character") || collider.gameObject.CompareTag("Player Bullet"))
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            if(collider.gameObject.GetComponent<Bullet>().crit == true)
            {
                health -= (collider.gameObject.GetComponent<Bullet>().damage);
            }
            else
            {
                health -= collider.gameObject.GetComponent<Bullet>().damage;
                Debug.Log(collider.gameObject.GetComponent<Bullet>().damage);
            }

            Destroy(collider.gameObject);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
