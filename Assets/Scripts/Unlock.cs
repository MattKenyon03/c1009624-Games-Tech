using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock : MonoBehaviour
{

    public GameObject thoughtBubble, rooms, maze, circle, mazeObject;
    private bool lockRange;
    public static string collisionName;

    //Picks up if the character is in the area where it can interact with the locked doors
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Contains("Lock Area"))
        {
            collisionName = collision.transform.parent.name;

            //Activates prompt to tell user to interact
            thoughtBubble.SetActive(true);
            lockRange = true;
        }
    }

    //Deactives the prompt when the user leaves the interactible area
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name.Contains("Lock Area"))
        {
            thoughtBubble.SetActive(false);
            lockRange = false;
        }
    }

    private void Update()
    {
        //Checks if the user is in the interactible area when they are pressing e to interact
        if(Input.GetKeyDown(KeyCode.E) && lockRange)
        {
            //Switches to the maze game
            rooms.SetActive(false);

            //Resets the maze and the circle to the current orientation
            Vector3 currentRotation = maze.transform.rotation.eulerAngles;
            mazeObject.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 105);
            circle.transform.localPosition = new Vector2(8.37f, -2.13f);

            maze.SetActive(true);
            lockRange = false;
        }
    }
}
