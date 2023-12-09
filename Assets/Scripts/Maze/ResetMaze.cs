using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMaze : MonoBehaviour
{

    public GameObject circle, maze;

    //Resets the map's rotation and the circle's transform so it can be replayed
    public void ResetButtonPressed()
    {
        maze.transform.rotation = Quaternion.Euler(0, 0, 105);
        circle.transform.localPosition = new Vector2(8.365209f, -2.13025f);
        circle.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
