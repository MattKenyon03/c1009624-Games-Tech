using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChooser : MonoBehaviour
{
    int index;
    public GameObject pistol, AR, rifle, shotgun;
    public static string activeGun;

    public void Start()
    {
        index = 0;
        activeGun = "Pistol";
    }

    //Increases the index of guns
    public void Increase()
    {
        if(index == 3)
        {
            index = 0;
        }
        else
        {
            index += 1;
        }
        GunChanger();
    }

    //Decreases the index of guns
    public void Decrease()
    {
        if (index == 0)
        {
            index = 3;
        }
        else
        {
            index -= 1;
        }
        GunChanger();
    }

    //Changes the guns due to the buttons pressed
    public void GunChanger()
    {
        pistol.SetActive(false);
        AR.SetActive(false);
        rifle.SetActive(false);
        shotgun.SetActive(false);

        if(index == 0)
        {
            pistol.SetActive(true);
            activeGun = "Pistol";
        }
        else if(index == 1)
        {
            AR.SetActive(true);
            activeGun = "AR";
        }
        else if (index == 2)
        {
            shotgun.SetActive(true);
            activeGun = "Shotgun";
        }
        else if(index == 3)
        {
            rifle.SetActive(true);
            activeGun = "Sniper";
        }
        
    }

}
