using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{

    public FlockBehaviour[] behaviours;
    public float[] weights;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //Handle Data Mismatch
        if(weights.Length != behaviours.Length)
        {
            return Vector2.zero;
        }

        //Set up move
        Vector2 move = Vector2.zero;

        //Iterate through behaviours
        for(int i = 0; i < behaviours.Length; i++)
        {
            // Calculate the partial movement vector for the current behavior, multiplied by weight
            Vector2 partialMove = behaviours[i].CalculateMove(agent, context, flock) * weights[i];

            //Adjust the partial movement if it exceeds the weight
            if (partialMove != Vector2.zero)
            {
                if(partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                //Add partial movement to total movement
                move += partialMove;
            }
        }

        // Return the movement vector
        return move;

    }
}
