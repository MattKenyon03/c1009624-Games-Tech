using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PathfindingNode : MonoBehaviour

{
    public List<PathfindingNode> allNeighbours = new List<PathfindingNode>();
    public float traversalCost;
    public int nodeVal;

    public float g;
    public float h;
    public PathfindingNode parent;

    private void Start()
    {
        FindNeighbours();
        SetupNode();
    }


    enum NodeType
    {
        Passable,
        Rough,
        Impassable,
        MAX
    }

    NodeType nodeType = NodeType.Passable;

    public bool IsImpassible()
    {
        return nodeType == NodeType.Impassable;
    }

    public float GetFScore()
    {
        return g + h;
    }

    //Gives vector directions in all 4 directions (1 unit away)
    static Vector3[] directions =
    {
        new Vector3(-1, 0, 0),
        new Vector3(1, 0, 0),
        new Vector3(0, -1, 0),
        new Vector3(0, 1, 0),
    };


    void FindNeighbours(bool recoonectingNeighbour = false)
    {
        //Clears previous list of neighbours
        allNeighbours.Clear();

        //Repeats checking process for all 4 directions
        foreach (Vector3 direction in directions)
        {
            RaycastHit info; //Gets information back from raycast
            if (Physics.Raycast(transform.position, direction, out info)) //Checks if there are any nodes in the locations checked
            {
                //Adds node to the list if there are ones next to the original
                PathfindingNode node = info.transform.gameObject.GetComponent<PathfindingNode>();
                if (node)
                {
                    allNeighbours.Add(node);
                }
            }
        }
    }

    
    void SetupNode()
    {
        if(nodeVal == -1)
        {
            nodeType = NodeType.Impassable;
        }
        switch (nodeType)
        {
            case NodeType.Passable:
                //Sets NavMesh area to 0
                GameObjectUtility.SetNavMeshArea(gameObject, 0);
                traversalCost = 1;
                break;
            case NodeType.Impassable:
                //Sets NavMesh area to 1
                GameObjectUtility.SetNavMeshArea(gameObject, 1);
                traversalCost = -1;
                break;
        }
    } 
}
