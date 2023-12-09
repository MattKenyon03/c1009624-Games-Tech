using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    //Switches to the selected scene when the button is pressed
    public void ButtonPressed(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
