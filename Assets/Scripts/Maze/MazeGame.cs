using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGame : MonoBehaviour
{

    public GameObject maze;
    public float rotationSpeed = 40f;
    private bool holdingA, holdingD;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            holdingA = true;
        }
        if(Input.GetKeyUp(KeyCode.A))
        {
            holdingA = false;
        }

        if(holdingA)
        {
            // Get the current rotation of the GameObject
            Vector3 currentRotation = maze.transform.rotation.eulerAngles;

            // Change the Z rotation based on the rotation speed and deltaTime
            float newZRotation = currentRotation.z + rotationSpeed * Time.deltaTime;

            // Apply the new rotation to the GameObject
            maze.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, newZRotation);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            holdingD = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            holdingD = false;
        }

        if (holdingD)
        {
            // Get the current rotation of the GameObject
            Vector3 currentRotation = maze.transform.rotation.eulerAngles;

            // Change the Z rotation based on the rotation speed and deltaTime
            float newZRotation = currentRotation.z - rotationSpeed * Time.deltaTime;

            // Apply the new rotation to the GameObject
            maze.transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, newZRotation);
        }

    }
}
