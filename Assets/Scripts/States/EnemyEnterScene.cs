using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This enemy state is activated first, and allows the enemy to move into the map from its spawnpoint
public class EnemyEnterScene : EnemyBaseState
{
    public float timer;

    public override void EnterState(PlayerTrackerAI enemy)
    {
        //Sets the destination of the pathfinding AI to the player
        enemy.destination = enemy.player;
    }

    public override void UpdateState(PlayerTrackerAI enemy)
    {

        if (enemy.transform.Find("Gun") == null)
        {
            enemy.currentState = enemy.moveToCoverState;
            enemy.moveToCoverState.EnterState(enemy);
        }

        //Sets a timer
        timer += Time.deltaTime;

        //When the timer reaches 2 seconds, the state is changed to allow the enemy to move to cover
        if (timer >= 2)
        {
            enemy.currentState = enemy.moveToCoverState;
            enemy.moveToCoverState.EnterState(enemy);
        }
    }
}
