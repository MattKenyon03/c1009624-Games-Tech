using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVector : MonoBehaviour
{
    public Vector2 coordinates;
    private GameObject player;
    public GameObject doorCheckers;

    private void Awake()
    {
        player = GameObject.Find("Character");
    }

    private void Update()
    {
        //Activates the current room's colliders that allow the user to switch into different rooms when going through doorways
        if(coordinates == player.GetComponent<RoomInstantiation>().currentPos)
        {
            doorCheckers.SetActive(true);
        }
        else
        {
            doorCheckers.SetActive(false);
        }
    }
}
