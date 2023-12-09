using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //If no neighbours, return no adjustment
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        //Add all points together and average
        Vector2 avoidMove = Vector2.zero;
        int avoid = 0;
        foreach (Transform item in context)
        {
            // Check if the distance between the agent and the neighboring agent is within the radius
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                avoid++;
                //Calculate the avoidance movement vector based on the difference in positions
                avoidMove += (Vector2)(agent.transform.position - item.position);
            }
            
        }

        //Calculate the average avoidance movement if there are agents to avoid
        if (avoid > 0)
        {
            avoidMove /= avoid;
        }

        // Return the movement vector.
        return avoidMove;

    }
}
