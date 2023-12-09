using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject controls, howToPlay, startScreen;

    public void ControlsButton()
    {
        controls.SetActive(true);
        startScreen.SetActive(false);
    }

    public void HowToPlayButton()
    {
        howToPlay.SetActive(true);
        startScreen.SetActive(false);
    }

    public void ReturnButton()
    {
        startScreen.SetActive(true);
        howToPlay.SetActive(false);
        controls.SetActive(false);
    }
}
