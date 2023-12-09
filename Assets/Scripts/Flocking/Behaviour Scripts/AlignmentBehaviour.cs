using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //If no neighbours, maintain current alignment
        if (context.Count == 0)
        {
            return agent.transform.up;
        }

        //Add all points together and average to calculate alignment
        Vector2 alignMove = Vector2.zero;
        foreach (Transform item in context)
        {
            alignMove += (Vector2)item.transform.up;
        }
        alignMove /= context.Count;

        //Returns the calculated alignment move
        return alignMove;
    }
}
