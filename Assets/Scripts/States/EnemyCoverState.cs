using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCoverState : EnemyBaseState
{

    public List<GameObject> viewNodeList = new();
    public int randomInt;

    public override void EnterState(PlayerTrackerAI enemy) 
    {
        enemy.GetComponent<Gun>().appropriateStateActive = false;
    }

    public override void UpdateState(PlayerTrackerAI enemy)
    {
        if (enemy.transform.Find("Circle").GetComponent<View>().canBeSeen == true)
        {
            if (enemy.transform.Find("Gun") == null)
            {
                enemy.currentState = enemy.moveToCoverState;
                enemy.moveToCoverState.EnterState(enemy);
            }
            else
            {
                enemy.currentState = enemy.shootState;
                enemy.shootState.EnterState(enemy);
            }
            
        }

        int randomOutcome = Random.Range(0, 2000);

        if(randomOutcome == 0 && enemy.transform.Find("Gun") != null)
        {
            enemy.currentState = enemy.attackPositionState;
            enemy.attackPositionState.EnterState(enemy);
        }
    }

}
