using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public TextMeshProUGUI pointsText, killsText, timeText, roomsText;
    public List<GameObject> checks, crosses;

    private void Awake()
    {
        //Outputs all progress made for achievements 
        roomsText.text = Score.rooms.ToString() + " / 30";

        float minutes = ((int)Score.secondsCount % 3600) / 60;
        float seconds = ((int)Score.secondsCount % 3600) % 60;
        timeText.text = minutes + ":" + seconds.ToString("00") + " / 5:00";

        pointsText.text = Score.points.ToString("0") + " / 500";

        killsText.text = Score.kills.ToString("0") + " / 20";

        //If the achievements are completed, they are checked, if not, they are crossed off
        if (Score.points >=500)
        {
            checks[0].SetActive(true);
        }
        else
        {
            crosses[0].SetActive(true);
        }
        if (Score.kills >= 20)
        {
            checks[1].SetActive(true);
        }
        else
        {
            crosses[1].SetActive(true);
        }
        if (Score.rooms >= 30)
        {
            checks[2].SetActive(true);
        }
        else
        {
            crosses[2].SetActive(true);
        }
        if (Score.secondsCount >= 300)
        {
            checks[3].SetActive(true);
        }
        else
        {
            crosses[3].SetActive(true);
        }
    }

    //Restarts the game
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
}
