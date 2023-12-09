using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new();
    public FlockBehaviour behaviour;

    [Range(1, 150)]
    public int startingCount;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f, 10f)]
    public float neighbourRadius = 1.5f;

    [RangeAttribute(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;

    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private void Awake()
    {
        //Randomly chooses number of objects in the flock
        startingCount = Random.Range(0, 6);

        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        //Loops through for each object in the flock
        for(int i = 0; i < startingCount; i++)
        {
            //Each object in the flock are instantiated
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                AgentDensity * startingCount * Random.insideUnitCircle,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                ) ;

            //Assigns specific name to the single agents
            newAgent.name = "Agent" + i;
            //Moves the agent to the centre of the flock
            newAgent.transform.localPosition = new Vector2(0, 0);
            //Adds agent to list of agents
            agents.Add(newAgent);
        }
    }
    private void Update()
    {
        //Loops through each object in the agent list to update them
        foreach(FlockAgent agent in agents)
        {
            //Gets a list of objects' transforms that are nearby the current agent
            List<Transform> context = GetNearbyObjects(agent);

            //Calculate the movement of the agent using the behaviour
            Vector2 move = behaviour.CalculateMove(agent, context, this);
            move *= driveFactor;

            //Limits the maximum speed of the agent
            if(move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }

            //Moves the agent based on the calculated move
            agent.Move(move);

        }
    }

    //Gets the list of nearby objects 
    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new();

        //Finds nearby colliders 
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadius);

        //Add each nearby object's transform to the list
        foreach (Collider2D c in contextColliders)
        {
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        //Returns the list to the update function
        return context;

    }
}
