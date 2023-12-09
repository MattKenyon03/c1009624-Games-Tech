using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeFinish : MonoBehaviour
{

    public GameObject rooms, maze;
    public GameObject character;
    public Vector2 currentPos;

    public GameObject thisDoor, otherDoor;
    public GameObject thisMapDoor, otherMapDoor;
    public GameObject pauseMenu, mapObjects, map;

    //Can be far more efficient
    private void OnTriggerEnter2D(Collider2D collision)
    {

        pauseMenu.SetActive(true);
        mapObjects.SetActive(true);

        if (collision.name == "Anti-Grav")
        {
            GetComponent<Rigidbody2D>().gravityScale *= -1;
        }

        if (collision.name == "Finish")
        {
            rooms.SetActive(true);
            currentPos = character.GetComponent<RoomInstantiation>().currentPos;

            if (Unlock.collisionName.Contains("Right"))
            {
                thisDoor = GameObject.Find("Rooms/" + currentPos + "/Doors Grid/Right Doorway");
                thisMapDoor = map.transform.Find(currentPos + "/Right Door").gameObject;

                thisDoor.SetActive(false);
                thisMapDoor.SetActive(false);

                Vector2 newPos = currentPos + new Vector2(1, 0);
                if (character.GetComponent<RoomInstantiation>().coordinates.Contains(newPos))
                {
                    otherDoor = GameObject.Find("Rooms/" + newPos + "/Doors Grid/Left Doorway");
                    otherDoor.SetActive(false);

                    otherMapDoor = map.transform.Find(newPos + "/Left Door").gameObject;
                    otherMapDoor.SetActive(false);
                }
            }

            if (Unlock.collisionName.Contains("Left"))
            {
                thisDoor = GameObject.Find("Rooms/" + currentPos + "/Doors Grid/Left Doorway");
                thisMapDoor = map.transform.Find(currentPos + "/Left Door").gameObject;

                thisDoor.SetActive(false);
                thisMapDoor.SetActive(false);

                Vector2 newPos = currentPos + new Vector2(-1, 0);
                if (character.GetComponent<RoomInstantiation>().coordinates.Contains(newPos))
                {
                    otherDoor = GameObject.Find("Rooms/" + newPos + "/Doors Grid/Right Doorway");
                    otherDoor.SetActive(false);

                    otherMapDoor = map.transform.Find(newPos + "/Right Door").gameObject;
                    otherMapDoor.SetActive(false);
                }
            }

            if (Unlock.collisionName.Contains("Top"))
            {
                thisDoor = GameObject.Find("Rooms/" + currentPos + "/Doors Grid/Top Doorway");
                thisMapDoor = map.transform.Find(currentPos + "/Top Door").gameObject;

                thisDoor.SetActive(false);
                thisMapDoor.SetActive(false);

                Vector2 newPos = currentPos + new Vector2(0, 1);
                if (character.GetComponent<RoomInstantiation>().coordinates.Contains(newPos))
                {
                    otherDoor = GameObject.Find("Rooms/" + newPos + "/Doors Grid/Bottom Doorway");
                    otherDoor.SetActive(false);

                    otherMapDoor = map.transform.Find(newPos + "/Bottom Door").gameObject;
                    otherMapDoor.SetActive(false);
                }
            }

            if (Unlock.collisionName.Contains("Bottom"))
            {
                thisDoor = GameObject.Find("Rooms/" + currentPos + "/Doors Grid/Bottom Doorway");
                thisMapDoor = map.transform.Find(currentPos + "/Bottom Door").gameObject;

                thisDoor.SetActive(false);
                thisMapDoor.SetActive(false);

                Vector2 newPos = currentPos + new Vector2(0, -1);
                if (character.GetComponent<RoomInstantiation>().coordinates.Contains(newPos))
                {
                    otherDoor = GameObject.Find("Rooms/" + newPos + "/Doors Grid/Top Doorway");
                    otherDoor.SetActive(false);

                    otherMapDoor = map.transform.Find(newPos + "/Top Door").gameObject;
                    otherMapDoor.SetActive(false);
                }
            }

            maze.SetActive(false);
            pauseMenu.SetActive(false);
            mapObjects.SetActive(false);
        }
    }
}
