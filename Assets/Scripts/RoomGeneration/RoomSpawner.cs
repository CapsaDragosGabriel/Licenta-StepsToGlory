using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDir;
    /**
     * 1->bot door
     * 2->top door
     * 3->left door
     * 4->right door
     */
    private int rand;
    private RoomTemplates roomTemplates;
    public bool spawned = false;
    private void Start()
    {
        Destroy(gameObject,4f);
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.4f);
    }
   
    private void Spawn()
    {
        if (!spawned)
        {
            if (openingDir == 1)

            {
                rand = Random.Range(0, roomTemplates.bottomRooms.Length);
                Instantiate(roomTemplates.bottomRooms[rand], transform.position, roomTemplates.bottomRooms[rand].transform.rotation);
            }
           else if (openingDir == 2)

            {
                rand = Random.Range(0, roomTemplates.topRooms.Length);
                Instantiate(roomTemplates.topRooms[rand], transform.position, roomTemplates.topRooms[rand].transform.rotation);
            }
          
            else if (openingDir == 3)

            {
                rand = Random.Range(0, roomTemplates.leftRooms.Length);
                    Instantiate(roomTemplates.leftRooms[rand], transform.position, roomTemplates.leftRooms[rand].transform.rotation);
               
            }
            else if (openingDir == 4)

            {
                rand = Random.Range(0, roomTemplates.rightRooms.Length);
                Instantiate(roomTemplates.rightRooms[rand], transform.position, roomTemplates.rightRooms[rand].transform.rotation);
            }
            spawned = true;

        }
    }


    /*
     private void OnTriggerEnter2D(Collider2D collision)
     {
         if (collision.CompareTag("RoomSpawn") )
         {
             if (collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)
             {
                 //Invoke("Spawn", 0.3f);
                 Destroy(collision.gameObject);

                 {
                     Instantiate(roomTemplates.secretRoom, transform.position, Quaternion.identity);

                 }

                if (spawned)
                 Destroy(gameObject);


             }
             spawned = true;

         }
     }
 */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoomSpawn"))
        {
            if (collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                //Invoke("Spawn", 0.3f);
                Debug.Log(roomTemplates);
                Debug.Log(transform.position);
               // if(roomTemplates!= null)
               if (roomTemplates==null) roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
                if(GameObject.FindGameObjectWithTag("Entry").transform.position!=this.transform.position)   
                Instantiate(roomTemplates.secretRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);


            }
            spawned = true;

        }
    }
}
