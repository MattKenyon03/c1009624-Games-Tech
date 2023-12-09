using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class RoomInstantiation : MonoBehaviour
{
    //Adds the starting room's coordinates to the list of visited coordinates
    public List<Vector2> coordinates = new()
    {
        { new Vector2( 0f, 0f )}
    };

    public Vector2 currentPos = new(0, 0); //The players's current room coordinates 
    public GameObject room; //Room prefab
    private Vector2 roomPos; //Room position

    public int numberOfEnemies;
    Vector2 newCoords, shiftCoords;
    public GameObject mapPiece, map, otherDoor, rooms;
    public Transform thisDoor, thisMapPiece;
    public List<int> potentialEnemyLocations;

    //Sets the first room and camera to the correct location
    public void Start() 
    {
        roomPos = new Vector2(0f, 0f);
        Camera.main.transform.localPosition = new Vector3(8.57f, -4.55f, -20);
    }

    private void FixedUpdate()
    {
        //Only spawns one enemy
        if(Score.secondsCount <= 240)
        {
            numberOfEnemies = 1;
        }
        //spawns two enemies
        if(Score.secondsCount > 240)
        {
            numberOfEnemies = 2;
        }
    }

    public int roomVal;

    private void OnTriggerEnter2D(Collider2D collision) //To Change rooms
    {
        potentialEnemyLocations = new();
        //Checks if the character has collided with a room changer game object
        //These objects are kept in all open doorways
        if (collision.CompareTag("Room Changer"))
        {
            Vector3 camPos = Camera.main.transform.localPosition;
            Vector2 mapPos = new(map.transform.localPosition.x, map.transform.localPosition.y);

            //New coordinates for the next room will be calculated
            //The position of the new room in the world will be generated
            //The camera will be translated so it lines up with the next room
            //The position of the map will be moved to keep the current room central
            switch (collision.name)
            {
                case "Top":
                    
                    newCoords = new Vector2(currentPos.x, (currentPos.y + 1));
                    roomPos += new Vector2(0f, 9.994f);
                    Camera.main.transform.localPosition = camPos + new Vector3(0f, 9.994f, 0);
                    map.transform.localPosition = mapPos + new Vector2(0, -100 * map.transform.localScale.y);
                    roomVal = 0;
                    break;

                case "Left":

                    newCoords = new Vector2(currentPos.x - 1, currentPos.y);
                    roomPos += new Vector2(-17.8925f, 0f);                    
                    Camera.main.transform.localPosition = camPos + new Vector3(-17.8925f, 0, 0);
                    map.transform.localPosition = mapPos + new Vector2(100 * map.transform.localScale.x, 0);
                    roomVal = 1;
                    break;

                case "Right":
                    newCoords = new Vector2(currentPos.x + 1, currentPos.y);
                    roomPos += new Vector2(17.8925f, 0f);
                    Camera.main.transform.localPosition = camPos + new Vector3(17.8925f, 0, 0);
                    map.transform.localPosition = mapPos + new Vector2(-100 * map.transform.localScale.x, 0);
                    roomVal = 2;
                    break;

                case "Bottom":

                    newCoords = new Vector2(currentPos.x, (currentPos.y - 1));
                    roomPos += new Vector2(0f, -9.994f);
                    Camera.main.transform.localPosition = camPos - new Vector3(0f, 9.994f, 0);
                    map.transform.localPosition = mapPos + new Vector2(0, 100 * map.transform.localScale.y);
                    roomVal = 3;
                    break;
            }

            //Checks if the new coordinates are in the list
            //If they are not, this means that a new room has to be generated
            if (!coordinates.Contains(newCoords))
            {
                GenerateNewRoom();
            }

            currentPos = newCoords;
            RoomEnabler();
        }
    }

    public void GenerateNewRoom()
    {
        Score.rooms += 1;

        //The next area is instantiated, scaled and named after its coordinates
        GameObject newRoom = Instantiate(room, roomPos, Quaternion.identity);
        newRoom.transform.SetParent(rooms.transform);
        newRoom.GetComponent<MapVector>().coordinates = newCoords;
        newRoom.name = (newCoords.ToString());

        List<GameObject> roomLists = new();

        Transform RoomTypes = newRoom.transform.Find("Disable Objects/Grid/Obstacle Layouts");

        //Gets the different layouts of rooms and stores them in a list
        foreach (Transform room in RoomTypes)
        {
            if (room.name != "Entrances")
            {
                roomLists.Add(room.gameObject);
            }
        }

        //Randomly chooses the room layout for the newly generated room
        int roomChosen = Random.Range(0, 5);
        for (int i = 0; i < 5; i++)
        {
            if (i == roomChosen)
            {
                roomLists[i].SetActive(true);
            }
        }

        //Generates room to be placed onto the map
        GameObject newMapPiece = Instantiate(mapPiece);
        newMapPiece.transform.SetParent(map.transform);
        newMapPiece.transform.localScale = new Vector3(1, 1, 1);
        newMapPiece.transform.localPosition = newCoords * 100;
        newMapPiece.name = (newCoords.ToString());

        //Iterates through all doorways and grabs all variables and game objects
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                thisDoor = newRoom.transform.Find("Doors Grid/Top Doorway");
                thisMapPiece = newMapPiece.transform.Find("Top Door");
                shiftCoords = newCoords + new Vector2(0, 1);
                otherDoor = GameObject.Find(shiftCoords.ToString() + "Doors Grid/Bottom Doorway");
            }
            else if (i == 1)
            {
                thisDoor = newRoom.transform.Find("Doors Grid/Left Doorway");
                thisMapPiece = newMapPiece.transform.Find("Left Door");
                shiftCoords = newCoords + new Vector2(-1, 0);
                otherDoor = GameObject.Find((newCoords + new Vector2(-1, 0)).ToString() + "Doors Grid/Right Doorway");
            }
            else if (i == 2)
            {
                thisDoor = newRoom.transform.Find("Doors Grid/Right Doorway");
                thisMapPiece = newMapPiece.transform.Find("Right Door");
                shiftCoords = newCoords + new Vector2(1, 0);
                otherDoor = GameObject.Find((newCoords + new Vector2(1, 0)).ToString() + "Doors Grid/Left Doorway");
            }
            else if (i == 3)
            {
                thisDoor = newRoom.transform.Find("Doors Grid/Bottom Doorway");
                thisMapPiece = newMapPiece.transform.Find("Bottom Door");
                shiftCoords = newCoords + new Vector2(0, -1);
                otherDoor = GameObject.Find((newCoords + new Vector2(0, -1)).ToString() + "Doors Grid/Top Doorway");
            }

            //Checks if the objects around the doors exist
            if (coordinates.Contains(shiftCoords))
            {
                //Checks if the other door is enabled if it does exist
                if (otherDoor.activeInHierarchy)
                {
                    //If it exists, check if it is locked
                    if (otherDoor.CompareTag("Locked"))
                    {
                        //If the door is locked, the door connecting to it has to be locked too
                        //The door is turned black, tagged with "locked", sets the lock area to true and colours 
                        thisDoor.gameObject.GetComponent<Tilemap>().color = new Color(0, 0, 0);
                        thisDoor.gameObject.tag = "Locked";
                        thisDoor.Find("Lock Area").gameObject.SetActive(true);
                        thisMapPiece.GetComponent<Image>().color = new Color(0, 0, 0);
                    }
                    thisDoor.gameObject.SetActive(true);
                    thisMapPiece.gameObject.SetActive(true);
                }
                else
                {
                    thisDoor.gameObject.SetActive(false);
                    thisMapPiece.gameObject.SetActive(false);
                }
            }
            else
            {
                RandomDoorGenerator(thisDoor, thisMapPiece);
            }

            if (!thisDoor.CompareTag("Locked") && !thisDoor.gameObject.activeInHierarchy && i != (3 - roomVal))
            {
                potentialEnemyLocations.Add(i);
            }

        }

        EnemyGenerator(newRoom);
        coordinates.Add(newCoords);
    }


    public void RoomEnabler()
    {
        //Finds all rooms that are active and puts them in a list
        GameObject[] roomListDelete = GameObject.FindGameObjectsWithTag("Room");

        //The current room is enabled and the others are disabled
        foreach (GameObject room in roomListDelete)
        {
            if (room.name != currentPos.ToString())
            {
                room.transform.Find("Disable Objects").gameObject.SetActive(false);
            }
            else if (room.name == currentPos.ToString())
            {
                room.transform.Find("Disable Objects").gameObject.SetActive(true);
            }
        }
    }

    public void RandomDoorGenerator(Transform door, Transform mapPiece)
    {
        //Generates a random number (1/2)
        int random1 = Random.Range(0, 2);

        //If 0, a door is put in the doorway of the current door transform
        //This is shown in the scene and the map
        if (random1 == 0)
        {
            door.gameObject.SetActive(true);
            mapPiece.gameObject.SetActive(true);

            int random2 = Random.Range(0, 2);

            //Another random value is generated determining if the door will be locked and interactible
            if(random2 == 0)
            {
                door.gameObject.GetComponent<Tilemap>().color = new Color(0, 0, 0);
                door.gameObject.tag = "Locked";
                thisDoor.Find("Lock Area").gameObject.SetActive(true);
                mapPiece.gameObject.GetComponent<Image>().color = new Color(0, 0, 0);
            }
        }
        else
        {
            //If there is no door, the doorway remains opened and disabled
            door.gameObject.SetActive(false);
            mapPiece.gameObject.SetActive(false);
        }
    }

    public void EnemyGenerator(GameObject room)
    {
        //Loops through for each enemy that is going to be spawned in
        for(int i = 0; i < numberOfEnemies; i++)
        {
            if (potentialEnemyLocations.Count != 0 && potentialEnemyLocations != null)
            {
                Debug.Log(potentialEnemyLocations.Count);
                int roomNum = Random.Range(0, potentialEnemyLocations.Count - 1);
                StartCoroutine(WaitForEnemySpawn(roomNum, room));
                potentialEnemyLocations.RemoveAt(roomNum);
            }
            
        }
    }

    //Function highlights where the enemies will be spawning in from so that the user can prepare, then activates the enemy
    public IEnumerator WaitForEnemySpawn(int roomNum, GameObject room)
    {
        //Checks what room the enemy will be spawned in from
        if (potentialEnemyLocations[roomNum] == 0)
        {
            room.transform.Find("Disable Objects/Top Warning").gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            room.transform.Find("Disable Objects/Top Enemy").gameObject.SetActive(true);
            room.transform.Find("Disable Objects/Top Warning").gameObject.SetActive(false);
        }
        else if (potentialEnemyLocations[roomNum] == 1)
        {
            room.transform.Find("Disable Objects/Left Warning").gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            room.transform.Find("Disable Objects/Left Enemy").gameObject.SetActive(true);
            room.transform.Find("Disable Objects/Left Warning").gameObject.SetActive(false);
        }
        else if (potentialEnemyLocations[roomNum] == 2)
        {
            room.transform.Find("Disable Objects/Right Warning").gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            room.transform.Find("Disable Objects/Right Enemy").gameObject.SetActive(true);
            room.transform.Find("Disable Objects/Right Warning").gameObject.SetActive(false);
        }
        else if (potentialEnemyLocations[roomNum] == 3)
        {
            room.transform.Find("Disable Objects/Bottom Warning").gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            room.transform.Find("Disable Objects/Bottom Enemy").gameObject.SetActive(true);
            room.transform.Find("Disable Objects/Bottom Warning").gameObject.SetActive(false);
        }
        
    }
}
