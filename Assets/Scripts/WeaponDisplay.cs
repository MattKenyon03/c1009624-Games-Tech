using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    public List<Sprite> gunSprites;
    public Image imageDisplay;
    public TextMeshProUGUI ammo;
    private GameObject player;
    public Slider health;

    private void Awake()
    {
        player = GameObject.Find("Character");
    }

    private void Update()
    {
        //Checks what gun is active, then resizes and displays the correct gun in the weapon box UI on screen
        if(player.GetComponent<Gun>().activeGun == "Pistol")
        {
            imageDisplay.sprite = gunSprites[0];
            imageDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(96.47f, 51.15f);
            imageDisplay.transform.localPosition = new Vector2(-13, -1.5f);
        }
        else if(player.GetComponent<Gun>().activeGun == "AR")
        {
            imageDisplay.sprite = gunSprites[1];
            imageDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(129.01f, 50.35f);
            imageDisplay.transform.localPosition = new Vector2(-22, 0);
        }
        else if(player.GetComponent<Gun>().activeGun == "Shotgun")
        {
            imageDisplay.sprite = gunSprites[2];
            imageDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(162.82f, 54.51f);
            imageDisplay.transform.localPosition = new Vector2(-1.9f, -0.9f);
        }
        else if(player.GetComponent<Gun>().activeGun == "Sniper")
        {
            imageDisplay.sprite = gunSprites[3];
            imageDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(173.02f, 46.31f);
            imageDisplay.transform.localPosition = new Vector2(0, 3.1f);
        }
        imageDisplay.transform.localPosition = new Vector2(0, 0);

        //Displays the ammo for the gun
        ammo.text = player.GetComponent<Gun>().currentAmmo.ToString();
        health.value = player.GetComponent<Health>().health / player.GetComponent<Health>().maxHealth;
    }
}
