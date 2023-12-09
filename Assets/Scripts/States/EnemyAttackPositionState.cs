using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This enemy state allows the enemy to track the user's movement and move into a position where they can be seen
//so they are ready to shoot at them
public class EnemyAttackPositionState : EnemyBaseState
{
    public List<GameObject> viewNodeList = new();
    public int randomInt;

    public override void EnterState(PlayerTrackerAI enemy)
    {
        //Disables the gun's shooting
        enemy.GetComponent<Gun>().appropriateStateActive = false;

        //Sorts through all the objects in the map that are raycasting to the player
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Raycast Areas");
        foreach(GameObject node in nodes)
        {
            //If the player can be seen from the object, that object is added to a list of nodes
            if(node.GetComponent<View>().canBeSeen == true)
            {
                viewNodeList.Add(node);
            }
        }

        //Checks if the list is empty or not
        if (viewNodeList.Count == 0)
        {
            Debug.Log("No nodes to move");
        }
        else
        {
            //A node from the list is then randomly selected
            randomInt = Random.Range(0, viewNodeList.Count);
            GameObject chosenNode = viewNodeList[randomInt];

            //The node selected is then set as the destination of the enemy
            enemy.destination = chosenNode;
        }
    }

    public override void UpdateState(PlayerTrackerAI enemy)
    {
        if (enemy.transform.Find("Gun") == null)
        {
            enemy.currentState = enemy.moveToCoverState;
            enemy.moveToCoverState.EnterState(enemy);
        }

        //If the player can see the enemy while moving, the state will change to a shooting state where the enemy can shoot at the player
        if (enemy.transform.Find("Circle").GetComponent<View>().canBeSeen == true)
        {
            enemy.currentState = enemy.shootState;
            enemy.shootState.EnterState(enemy);
        }
    }
}
