using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used to make a raycast from the current object to the user, to see if it is in the line of sight

//This script helps with the pathfinding AI by setting up raycasts from 6 points in each room, and seeing if the
//user can be seen from them. The result will then determine where the enemy moves to in order to gain a better 
//positioning against the player
public class View : MonoBehaviour
{
    Transform character, thisObject;
    public bool canBeSeen;

    private void Awake()
    {
        //Finds the character object and object the script is in
        character = GameObject.Find("Character").transform;
        thisObject = gameObject.transform;
    }

    public void Update()
    {
        //Draws a line from in front of the object to the player's position
        RaycastHit2D hit = Physics2D.Raycast(thisObject.position, -(thisObject.position - character.position), Mathf.Infinity);

        //Draws a line in the editor for debugging
        Debug.DrawRay(character.position, thisObject.position - character.position, Color.red);
        
        //If the raycast comes into contact with any obstacles, then the user cannot be seen from this point
        if(hit.collider != null && hit.collider.name.Contains("Obstacles"))
        {
            canBeSeen = false;
        }
        //If the raycast doesn't collide with any obstacles, the user can be seen from this point
        else if(hit.collider == null || hit.collider.name != "Obstacles")
        {
            if(!hit.collider.name.Contains("Enemy"))
            {
                canBeSeen = true;
            }
        }
    }
    
}
