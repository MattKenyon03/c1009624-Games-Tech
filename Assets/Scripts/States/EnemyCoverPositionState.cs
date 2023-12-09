using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCoverPositionState : EnemyBaseState
{

    public List<GameObject> viewNodeList = new();
    public int randomInt;

    public override void EnterState(PlayerTrackerAI enemy)
    {
        enemy.GetComponent<Gun>().appropriateStateActive = false;
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Raycast Areas");
        foreach (GameObject node in nodes)
        {
            if (node.GetComponent<View>().canBeSeen == false)
            {
                viewNodeList.Add(node);
            }
        }

        if (viewNodeList.Count == 0)
        {

        }
        else
        {
            randomInt = Random.Range(0, viewNodeList.Count);
            GameObject chosenNode = viewNodeList[randomInt];
            enemy.destination = chosenNode;
        }
    }

    public override void UpdateState(PlayerTrackerAI enemy)
    {
        if (enemy.transform.Find("Circle").GetComponent<View>().canBeSeen == false)
        {
            enemy.currentState = enemy.coverState;
            enemy.coverState.EnterState(enemy);
        }
    }
}
