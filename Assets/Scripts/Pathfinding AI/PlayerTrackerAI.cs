using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrackerAI : MonoBehaviour
{
    [SerializeField]
    LineRenderer lines;

    [SerializeField]
    GameObject startingPosition;

    [SerializeField]
    public GameObject destination;

    [SerializeField]
    public List<GameObject> coverDestinations = new();

    [SerializeField]
    public List<GameObject> shootingDestinations = new();

    List<PathfindingNode> path = new();

    public GameObject player;

    public EnemyBaseState currentState;

    [Header("States")]
    public EnemyCoverState coverState = new();
    public EnemyCoverPositionState moveToCoverState = new();
    public EnemyShootingState shootState = new();
    public EnemyAttackPositionState attackPositionState = new();
    public EnemyEnterScene enterState = new();

    private void Start()
    {
        //Find the player object
        player = GameObject.Find("Character");
        //Check and set the initial state
        CheckState();
    }

    private void Update()
    {
        // Find a path from the starting position to the destination
        FindPath(startingPosition.transform.position, destination.transform.position);

        //Build path graphics
        BuildPathGraphics();

        //Update the current state.
        currentState.UpdateState(this);
    }

    //Gets the pathfinding node for a position using a raycast
    PathfindingNode GetNodeForPosition(Vector3 startingPos)
    {
        //Performs a raycast 1 unit down
        if (Physics.Raycast(startingPos, Vector3.down, out RaycastHit info))
        {
            //Get the pathfinding script attached to the object that collided with the raycast
            return info.transform.GetComponent<PathfindingNode>();
        }
        return null;
    }

    // Calculate the distance between a node and the destination
    float CalculateHeuristic(PathfindingNode n)
    {
        return (n.transform.position - destination.transform.position).magnitude;
    }

    //Find the path
    bool FindPath(Vector3 startingPos, Vector3 destinationPos)
    {
        //Clear the path list
        path.Clear();

        //Get the starting and destination nodes
        PathfindingNode startNode = GetNodeForPosition(startingPos);
        PathfindingNode destNode = GetNodeForPosition(destinationPos);

        if (!startNode || !destNode)
        {
            return false;
        }

        //Initialise start node details
        startNode.parent = null;
        startNode.g = 0.0f;
        startNode.h = CalculateHeuristic(startNode);

        //Create open and closed lists for nodes
        List<PathfindingNode> openList = new();
        List<PathfindingNode> closedList = new();

        openList.Add(startNode);


        while (openList.Count > 0)
        {
            //FInd the node with the lowest F score in the open list
            PathfindingNode node = openList[0];

            for (int i = 1; i < openList.Count; ++i)
            {
                // Iterate through the open list to find the node with the lowest F score
                if (openList[i].GetFScore() < node.GetFScore())
                {
                    node = openList[i];
                }
            }

            //If the destination node is reached, reconstruct the path and return true
            if (node == destNode)
            {
                while (node)
                {
                    //Reconstruct the path by backtracking through the parent nodes so the enemy can follow it
                    path.Add(node);
                    node = node.parent;
                }
                return true;
            }

            //Explore neighboring nodes
            foreach (PathfindingNode n in node.allNeighbours)
            {
                if (closedList.Contains(n))
                {
                    // Skip nodes that have already been explored and added to the closed list
                    continue;
                }
                if (n.IsImpassible())
                {
                    // If a node is impassable, it is moved to the closed list
                    closedList.Add(node);
                    continue;
                }

                // Calculate new scores and update node properties if a better path is found.
                float newH = CalculateHeuristic(n);
                float newG = node.GetFScore() + n.traversalCost;
                float newF = newG + newH;
                bool inList = openList.Contains(n);

                if (newF < node.GetFScore() || !inList)
                {
                    if (!inList)
                    {
                        // If the node is not in the open list, add it and update its heuristic
                        n.h = newH;
                        n.h = newH;
                        openList.Add(n);
                    }
                    // Update node properties for a potentially better path
                    n.g = newG;
                    n.h = newH;
                    n.parent = node;
                }
            }
            // Move the current node to the closed list
            openList.Remove(node);
            closedList.Add(node);
        }
        return false;
    }

    public GameObject enemy;
    public Rigidbody2D rb;
    public float acceleration;
    public float maxSpeed;
    Vector2 movement;

    void BuildPathGraphics()
    {
        //Array that stores path positions
        Vector3[] positions = new Vector3[path.Count];

        //Offsets the path position to be one block above the nodes
        for (int i = 0; i < path.Count; i++)
        {
            positions[i] = path[i].transform.position + Vector3.up;
        }

        //Line renderer visualised the path to take
        lines.positionCount = path.Count;
        lines.SetPositions(positions); //Need to get it to follow lines

        //Moving from (length - 1) --> (length - 2)

        if (lines.positionCount > 1)
        {
            movement = lines.GetPosition(lines.positionCount - 2) - enemy.transform.position;

            //Apply the force due to direction and acceleration to the enemy
            rb.AddForce(movement * acceleration);

            //Limit the velocity to the max speed
            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            if (Mathf.Abs(rb.velocity.y) > maxSpeed)
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * maxSpeed);
        }
        else
        {
            //Stop the enemy if the path is empty
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    public bool covering;

    //Set the initial state
    void CheckState()
    {
        currentState = enterState;
        currentState.EnterState(this);
    }
}
