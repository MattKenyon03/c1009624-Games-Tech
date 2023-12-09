using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static float kills, time, rooms, points;
    public static float secondsCount;
    public GameObject pauseMenu, winMenu, ingameMenu;
    public static bool gameCompleted;

    public void Start()
    {
        points = 0;
        kills = 0;
        secondsCount = 0;
        rooms = 0;
        //Boolean for when the game finishes
        gameCompleted = false;
    }

    private void FixedUpdate()
    {
        //Calculates the points
        points = (kills * 10) + (secondsCount) + (rooms * 5);

        //Calculates time in game
        if (!pauseMenu.activeInHierarchy)
        {
            secondsCount += Time.deltaTime;
        }

        //Checks if the criteria has been met to complete the game
        if(points >= 500 && kills >= 20 && rooms >= 30 && secondsCount > 300 && gameCompleted == false)
        {
            //If the game is won, the win menu comes up
            Time.timeScale = 0;
            winMenu.SetActive(true);
            ingameMenu.SetActive(false);
            pauseMenu.SetActive(false);
            gameCompleted = true;
        }
    }
}
