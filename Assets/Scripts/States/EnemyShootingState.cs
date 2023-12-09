using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This enemy state is used for shooting at the player
public class EnemyShootingState : EnemyBaseState
{
    public override void EnterState(PlayerTrackerAI enemy)
    {
        //Sets the destination to itself so the enemy stays stationary
        enemy.destination = enemy.gameObject;
        //Activates the gun's shootng so the enemy can shoot at the user
        enemy.GetComponent<Gun>().appropriateStateActive = true;
    }

    public override void UpdateState(PlayerTrackerAI enemy)
    {

        if (enemy.transform.Find("Gun") == null)
        {
            enemy.currentState = enemy.moveToCoverState;
            enemy.moveToCoverState.EnterState(enemy);
        }

        //If the enemy has run out of ammo, it will change states to find cover 
        if (enemy.GetComponent<Gun>().currentAmmo == 0)
        {
            enemy.currentState = enemy.coverState;
            enemy.moveToCoverState.EnterState(enemy);
        }

        //If the enemy can no longer see the player, it will either move to find them again, or find cover
        if(enemy.transform.Find("Circle").GetComponent<View>().canBeSeen == false)
        {
            //Randomises the outcome
            int randomOutcome = Random.Range(0, 2);

            if(randomOutcome == 0)
            {
                enemy.currentState = enemy.attackPositionState;
                enemy.attackPositionState.EnterState(enemy);
            }
            else
            {
                enemy.currentState = enemy.moveToCoverState;
                enemy.moveToCoverState.EnterState(enemy);
            }
        }
    }
}
