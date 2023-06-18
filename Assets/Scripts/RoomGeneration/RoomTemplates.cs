using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;

    public GameObject[] leftRooms;

    public GameObject[] rightRooms;
    public GameObject secretRoom;

    public List<GameObject> rooms;
    public GameObject portal;

    bool spawnedPortal = false;
    public GameObject  portalInstance;

    public List<GameObject> Spawns = new List<GameObject>();
    public GameObject barrel;
    public int randomFactor;
    private void SpawnEnemies()
    {
        bool skipfirst = false;
        print(rooms.Count);
        for (int index=1; index<rooms.Count; index++)
        {

            //if(room.GetComponentInChildren<ProceduralSpawner>()!=null)
            rooms[index].GetComponentInChildren<ProceduralSpawner>().delayedSpawn();
        //    Debug.Log(rooms[index].GetComponentInChildren<ProceduralSpawner>().getPlaces().Count);
        }
       /* foreach ( var room in rooms)
        {
            if(!skipfirst)
            {
                skipfirst= true;
                continue;
            }
            //if(room.GetComponentInChildren<ProceduralSpawner>()!=null)
            room.GetComponentInChildren<ProceduralSpawner>().delayedSpawn();
            Debug.Log(room.GetComponentInChildren<ProceduralSpawner>().getPlaces().Count);

        }*/
    }
    private void UpdateAstar()
    {

        var astar = GameObject.FindGameObjectWithTag("ToDestroy").GetComponent<AstarPath>();
        if (astar)
            astar.Scan();

    }
    private void SpawnPortal()
    {
      
            portalInstance = Instantiate(portal, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
      
    }
    public void Awake()
    {
       Invoke("SpawnEnemies", 4.5f);
        Invoke("UpdateAstar", 4f);
        Invoke("SpawnPortal", 4.5f);

    }

}
