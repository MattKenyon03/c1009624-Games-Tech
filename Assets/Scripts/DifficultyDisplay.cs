using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DifficultyDisplay : MonoBehaviour
{

    public TextMeshProUGUI difficultyText;
    public GameObject background, questionButton;

    public void FixedUpdate()
    {
        //Fetches seconds count 
        float time = Score.secondsCount;

        //Outputs the difficulty text and changes the colour of the object depending on time played
        if (time <= 60)
        {
            difficultyText.text = "Easy";
            background.GetComponent<Image>().color = new Color(0, 0.8584f, 0);
        }
        else if (time > 60 && time <= 120)
        {
            difficultyText.text = "Medium";
            background.GetComponent<Image>().color = new Color(0.8584f, 0.4038f, 0);
        }
        else if (time > 120 && time <= 240)
        {
            difficultyText.text = "Hard";
            background.GetComponent<Image>().color = new Color(0.8588f, 0, 0);
        }
        else
        {
            difficultyText.text = "Very Hard";
            difficultyText.color = new Color(1, 1, 1);
            background.GetComponent<Image>().color = new Color(0.4245f, 0, 0.0437f);
            questionButton.GetComponent<Image>().color = new Color(1, 1, 1);
        }
    }

    public GameObject difficultyMenu;

    //Activates the menu when the button is pressed
    public void ButtonPressed()
    {
        Time.timeScale = 0;
        difficultyMenu.SetActive(true);
    }

    //Deactivates the menu when the button is pressed
    public void ReturnButton()
    {
        Time.timeScale = 1;
        difficultyMenu.SetActive(false);
    }
}
