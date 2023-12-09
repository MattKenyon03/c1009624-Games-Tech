using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{

    public GameObject winMenu, ingameMenu;

    //Button allows the player to carry on playing infinitely until they die
    public void ContinueButton()
    {
        Time.timeScale = 1;
        winMenu.SetActive(false);
        ingameMenu.SetActive(true);
    }

    //Button restarts the game for the user to play again
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
}
