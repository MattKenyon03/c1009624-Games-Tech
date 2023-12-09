using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Stay On Screen")]
public class StayOnScreen : FlockBehaviour
{
    public Vector2 centre;
    public float radius;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //Calculate the vector offset from the agent's position to the center of the flocking circle area
        Vector2 centreOffset = centre - (Vector2)agent.transform.position;
        float t = centreOffset.magnitude / radius;

        //If the agent is within 0.9 of the circle's radius, return a zero vector
        if (t < 0.9f)
        {
            return Vector2.zero;
        }
        
        //If the agent is outside of this range, apply an vector towards the center of the circle leading it back to the centre
        return t * t * centreOffset;
    }

}
