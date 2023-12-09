using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCollider : MonoBehaviour
{ 
    public GameObject enemy, gunParticles;

    //Checks if the bullet hits the gun, which destroys it
    public void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("Player Bullet"))
        {
            enemy.GetComponent<Gun>().enabled = false;
            gunParticles.SetActive(true);
            Destroy(gameObject);
        }
    }
}
