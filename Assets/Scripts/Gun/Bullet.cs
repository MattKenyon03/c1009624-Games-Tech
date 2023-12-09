using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public GameObject bullet;
    public bool crit;

    Vector3 direction, rotation;
    public GameObject player;
    public GameObject objectShooting;
    public float inaccuracyRange;
    public float damage;

    //Stores all the damage done by each weapon
    public static Dictionary<string, int> damageValues = new()
    {
        { "Pistol", 50 },
        { "AR", 35 },
        { "Shotgun", 40 },
        { "Sniper", 150 }
    };

    void Start()
    {
        player = GameObject.Find("Character");
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();

        //Gets the position of the mouse on the screen
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        //Chooses random number between 0 and 20
        int random = Random.Range(0, 20);
        
        //If the number is 0, the bullet will be a crit hit, which doubles the damage, and makes the bullet RED
        if (random == 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            crit = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
            crit = false;
        }

        float shotgunSpray;

        //If the shotgun item is active, the spray of bullets will be randomised from -1.5 - 1.5
        if (objectShooting.GetComponent<Gun>().activeGun == "Shotgun")
        {
            shotgunSpray = Random.Range(-1.5f, 1.5f);
        }
        else
        {
            shotgunSpray = 0;
        }

        if (inaccuracyRange > 0)
        {
            inaccuracyRange = (-0.01333f * Score.secondsCount) + 5;
        }
        else
        {
            inaccuracyRange = 0;
        }

        damage = damageValues[objectShooting.GetComponent<Gun>().activeGun];
        
        float inaccuracy = Random.Range(-inaccuracyRange, inaccuracyRange);

        //Calculates the direction of the bullet, taking in the shotgun spray if it is active
        if (CompareTag("Player Bullet"))
        {
            direction = mousePos - transform.position + new Vector3(shotgunSpray, shotgunSpray, 0);
            rotation = transform.position - mousePos;
        }
        else
        {
            direction = player.transform.position - transform.position + new Vector3(shotgunSpray, shotgunSpray, 0) + new Vector3(inaccuracy, inaccuracy, 0);
            rotation = transform.position - player.transform.position;
        }

        //Adds force and rotation to the object so it flies in the right direction
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rotate = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotate + 90);

        
        StartCoroutine(Destroy());
    }

    //Destroys the bullet when it has been active for 1 second
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(bullet);
    }

    //Destroys the bullet if it comes into contact with anything
    private void OnCollisionEnter2D(Collision2D collider)
    {
        Destroy(bullet);
    }

}
