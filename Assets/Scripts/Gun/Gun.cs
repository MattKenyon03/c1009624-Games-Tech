using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    
    public bool canFire;
    public Transform bulletTransform;
    public GameObject bullet;
    public GameObject thisCharacter;
    private float timer;
    private float timeBetweenFiring;

    public string activeGun;

    public List<Sprite> gunTopSprites;
    public SpriteRenderer gunTopSprite;
    public int currentAmmo;

    public int force;
    public bool appropriateStateActive;

    //Dictionary storing ammount of current ammunition per gun
    public Dictionary<string, int> ammunition = new()
    {
        {"Pistol", 16},
        {"AR", 50},
        {"Shotgun", 8},
        {"Sniper", 5}
    };

    //Dictionary storing max ammount of current ammunition per gun
    public Dictionary<string, int> ammunitionMax = new()
    {
        { "Pistol", 16 },
        { "AR", 50 },
        { "Shotgun", 8 },
        { "Sniper", 5 }
    };

    //Dictionary storing reload times of each gun
    public Dictionary<string, int> reloadTimes = new()
    {
        { "Pistol", 3 },
        { "AR", 5 },
        { "Shotgun", 4 },
        { "Sniper", 6 }
    };

    public Dictionary<string, float> gunWeights = new()
    {
        { "Pistol", 1 },
        { "AR", 1.5f },
        { "Shotgun", 2 },
        { "Sniper", 1.8f }
    };

    public Dictionary<string, float> timeBetweenFiringValues = new()
    {
        { "Pistol", 0.6f },
        { "AR", 0.2f },
        { "Shotgun", 1.5f },
        { "Sniper", 2f }
    };

    public Dictionary<string, int> forceValues = new()
    {
        { "Pistol", 20 },
        { "AR", 35 },
        { "Shotgun", 15 },
        { "Sniper", 75 }
    };

    private void Awake()
    {
        if(name == "Character")
        {
            appropriateStateActive = true;
        }
        player = GameObject.Find("Character");

        GunSetup();
    }

    //Determines what gun the current object will use, and allocates all necessary values
    public void GunSetup()
    {
        if (name != "Character")
        {
            int gunVal;

            //Randomly assigns gun to the enemy
            //The enemy gets a choice of more powerful guns as the game goes on
            if (Score.secondsCount <= 60)
            {
                gunVal = 0;
            }
            else if (Score.secondsCount > 60 && Score.secondsCount <= 120)
            {
                gunVal = Random.Range(0, 2);
            }
            else if (Score.secondsCount > 120 && Score.secondsCount <= 240)
            {
                gunVal = Random.Range(0, 3);
            }
            else
            {
                gunVal = Random.Range(0, 4);
            }

            switch (gunVal)
            {
                case 0:
                    activeGun = "Pistol";
                    break;
                case 1:
                    activeGun = "AR";
                    break;
                case 2:
                    activeGun = "Shotgun";
                    break;
                case 3:
                    activeGun = "Sniper";
                    break;
            }
        }
        else
        {
            //Assigns chosen gun to the user
            activeGun = GunChooser.activeGun;
        }

        //Assigns all sprites, timings and force to each gun
        if (activeGun == "Pistol")
        {
            gunTopSprite.sprite = gunTopSprites[0];
        }
        else if (activeGun == "AR")
        {
            gunTopSprite.sprite = gunTopSprites[1];
        }
        else if (activeGun == "Shotgun")
        {
            gunTopSprite.sprite = gunTopSprites[2];
        }
        else if (activeGun == "Sniper")
        {
            gunTopSprite.sprite = gunTopSprites[3];
        }

        //Adds different weights to the guns
        thisCharacter.GetComponent<Rigidbody2D>().mass = gunWeights[activeGun];
        timeBetweenFiring = timeBetweenFiringValues[activeGun];
        force = forceValues[activeGun];
    }

    private void FixedUpdate()
    {
        currentAmmo = ammunition[activeGun];

        Rotation();
        Reloading();

        if(appropriateStateActive)
        {
            Firing();
        }

    }

    public void Reloading()
    {
        //If the character is reloading, it will shop the duration in the in game menu slider
        if (name == "Character")
        {
            if (reloadSlider.activeInHierarchy)
            {
                //If the timer hasn't ended, it will be shown in the menu
                if (timeElapsed < duration)
                {
                    if (name == "Character")
                    {
                        reloadSlider.GetComponent<Slider>().value = Mathf.Lerp(0, 1, timeElapsed / duration);
                    }
                    timeElapsed += Time.deltaTime;
                }
                //If the timer has ended, the slider will no longer be shown and the ammo will be reset, and shown in the menu
                else
                {
                    if (name == "Character")
                    {
                        ammoText.SetActive(true);
                        reloadSlider.SetActive(false);
                    }
                    ammunition[activeGun] = ammunitionMax[activeGun];
                    reloading = false;
                }
            }
        }
        //If the enemy is reloading, nothing is shown in the menu
        else if (name != "Character")
        {
            if (reloading)
            {
                //When the timer runs out, the enemy will be able to shoot again
                if (timeElapsed < duration)
                {
                    timeElapsed += Time.deltaTime;
                }
                else
                {
                    ammunition[activeGun] = ammunitionMax[activeGun];
                    reloading = false;
                }
            }

        }
    }

    private bool reloading;
    Vector2 direction;
    private GameObject player;

    private void Rotation()
    {
        //Rotates the player to point towards the mouse

        if(name == "Character")
        {
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }
        else
        {
            direction = player.transform.position - transform.position;
        }
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        //Checks if the gun is ready to fire yet
        if (!canFire && appropriateStateActive)
        {
            //Adds time to the timer
            timer += Time.deltaTime;
            //Checks if the timer has passed the time between firing
            if (timer > timeBetweenFiring)
            {
                //Allows the gun to fire again snd resets the timer
                canFire = true;
                timer = 0;
            }
        }
    }

    public GameObject bulletObject;

    public GameObject reloadSlider, ammoText;

    private float timeElapsed;
    private float duration;
    public void Firing()
    {
        //Checks the conditions for both the user and the enemy to shoot
        if ((name == "Character" && Input.GetMouseButton(0) && canFire) || (name != "Character" && canFire && !GetComponent<PlayerTrackerAI>().covering))
        {
            int tempAmmo;
            //Checks if there is any ammunition in the current gun
            if (ammunition[activeGun] > 0)
            {
                //Subtracts 1 from the ammo count for the current gun
                tempAmmo = ammunition[activeGun];
                tempAmmo--;
                ammunition[activeGun] = tempAmmo;

                //If the shotgun is chosen, it will generate 5 bullets, so the script loops 5 times
                if (activeGun == "Shotgun")
                {
                    for (int i = 0; i < 5; i++)
                    {
                        //Instantiates the bullet and adds all the necessary details to the object
                        bulletObject = Instantiate(bullet, bulletTransform.position, Quaternion.identity);
                        Bullet bulletRef = bulletObject.GetComponent<Bullet>();
                        bulletRef.objectShooting = thisCharacter;
                        bulletRef.force = force;

                        //Tags the bullet so that players and enemies can't hurt themselves
                        if (name == "Character")
                        {
                            bulletObject.tag = "Player Bullet";
                        }
                        else
                        {
                            bulletObject.tag = "Enemy Bullet";
                        }  
                    }
                }
                else
                {
                    //Instantiates the bullet and adds all the necessary details to the object
                    bulletObject = Instantiate(bullet, bulletTransform.position, Quaternion.identity);
                    Bullet bulletRef = bulletObject.GetComponent<Bullet>();
                    bulletRef.objectShooting = thisCharacter;
                    bulletRef.force = force;

                    //Tags the bullet so that players and enemies can't hurt themselves
                    if (name == "Character")
                    {
                        bulletObject.tag = "Player Bullet";
                    }
                    else
                    {
                        bulletObject.tag = "Enemy Bullet";
                    }
                }
            }
            //Reloads the gun if the ammo count is 0
            else if (ammunition[activeGun] == 0 && reloading == false)
            {
                reloading = true;
                //Sets the ammo and reload slider in the in game menu
                if (name == "Character")
                {
                    ammoText.SetActive(false);
                    reloadSlider.SetActive(true);
                }

                //Sets the duration of the 
                duration = reloadTimes[activeGun];
                timeElapsed = 0;
            }

            canFire = false;
        }
    }

    
}
