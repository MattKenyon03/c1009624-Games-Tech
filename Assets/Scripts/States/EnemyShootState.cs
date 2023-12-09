using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootState : EnemyBaseState
{
    public override void EnterState(PlayerTrackerAI enemy)
    {
        enemy.destination = enemy.shootingDestinations[0];
    }

    public override void UpdateState(PlayerTrackerAI enemy)
    {
        int probability = Random.Range(0, 2000);

        if (enemy.transform.Find("Gun") == null)
        {
            enemy.currentState = enemy.moveToCoverState;
            enemy.moveToCoverState.EnterState(enemy);
        }
        else
        {
            if (probability == 0)
            {
                enemy.currentState = enemy.coverState;
                enemy.coverState.EnterState(enemy);
            }
        }
    }
}
