using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private GameObject player;
    public GameObject gameOverScreen, ingameMenu;

    private void Start()
    {
        player = GameObject.Find("Character");
    }

    void Update()
    {
        //If the player's health reaches 0, the game is over and the game over menu is shown
        if(player.GetComponent<Health>().health <= 0)
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
            ingameMenu.SetActive(false);
        }
    }
}
