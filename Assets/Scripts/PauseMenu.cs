using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu, inGameMenu;
    public TextMeshProUGUI pointsText, killsText, timeText, roomsText;
    public GameObject pointsCheck, enemiesCheck, roomsCheck, timeCheck;

    private void Awake()
    {
        //Activates the pause menu
        pauseMenu.SetActive(false);
        inGameMenu.SetActive(true);
    }

    private void Update()
    {
        //If escape key is pressed, the pause menu is toggled
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenu.activeInHierarchy)
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                inGameMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 0;
                map.SetActive(false);
                statsScreen.SetActive(true);
                pauseMenu.SetActive(true);
                inGameMenu.SetActive(false);
            }
        }

        //All the values for the achievements are shown in text
        roomsText.text = Score.rooms.ToString() + " / 30";

        float minutes = ((int)Score.secondsCount % 3600) / 60;
        float seconds = ((int)Score.secondsCount % 3600) % 60;
        timeText.text = minutes + ":" + seconds.ToString("00") + " / 5:00";

        pointsText.text = Score.points.ToString("0") + " / 500";

        killsText.text = Score.kills.ToString("0") + " / 20";

        //The achievements are checked if they have been met
        if(Score.points >= 500)
        {
            pointsCheck.SetActive(true);
        }
        if(Score.kills >= 20)
        {
            enemiesCheck.SetActive(true);
        }
        if(Score.rooms >= 30)
        {
            roomsCheck.SetActive(true);
        }
        if(Score.secondsCount >= 300)
        {
            timeCheck.SetActive(true);
        }

    }

    public GameObject map, statsScreen;

    //Toggles the map
    public void MapButton()
    {
        if(!map.activeInHierarchy)
        {
            map.SetActive(true);
            statsScreen.SetActive(false);
        }
        else
        {
            map.SetActive(false);
            statsScreen.SetActive(true);
        }
    }

    //Button returns the user to the menu
    public void ReturnButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
