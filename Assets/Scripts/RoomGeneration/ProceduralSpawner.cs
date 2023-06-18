using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Spawns = new List<GameObject>();
    private int randomFactor;
    private Tilemap tileMap;
  //  [SerializeField]
    public List<Vector3> availablePlaces=new List<Vector3>();
    bool spawned = false;

    public float x, y, xreal, yreal;

    private RoomTemplates templates;
    public GameObject barrel;
    public List<Vector3> getPlaces() { return availablePlaces; }
    void OnEnable()
    {


      // Invoke("delayedSpawn", 3f);

    }
    public void delayedSpawn()
    {
    //    Debug.Log("EnteredDelaySpawn");
            //print(SceneManager.GetActiveScene().name);
            spawned = true;
            tileMap = this.gameObject.GetComponent<Tilemap>();
      // Debug.Log(tileMap.transform.position);
            templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Debug.Log(templates);
            Spawns = templates.Spawns;
        barrel = templates.barrel;
            randomFactor = templates.randomFactor;
            FindLocationsOfTiles();
     //       SpawnEnemies();
        
    }
    IEnumerator WaitBeforeSpawn()
    {
        spawned = false;
        while (!GameObject.FindGameObjectWithTag("Player"))
            yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(3f);
        // print("tried");
        // print(SceneManager.GetActiveScene().name);

        if (!spawned)
        {
            tileMap = GetComponent<Tilemap>();
            spawned = true;
            FindLocationsOfTiles();
        }
    }
    public void ForceSpawn()
    {
     //   StartCoroutine("WaitBeforeSpawn");

    }
    private void FindLocationsOfTiles()
    {
        //  availablePlaces = new List<Vector3>(); // create a new list of vectors by doing...
        //Debug.Log("se incearca spawnu");

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++) // scan from left to right for tiles
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++) // scan from bottom to top for tiles
            {
                Vector3Int localPlace = new Vector3Int(n, p, 0); // if you find a tile, record its position on the tile map grid
                Vector3 place = tileMap.CellToWorld(localPlace); // convert this tile map grid coords to local space coords
               // Debug.Log(localPlace);
                //Debug.Log(place);
              //  Debug.Log(tileMap.HasTile(localPlace));

                if (tileMap.HasTile(localPlace) )
                {
                    //Tile at "place"
                    availablePlaces.Add(place);
                    //  Debug.Log(availablePlaces.Count);
                }


            }
        }
      // if (availablePlaces.Count == 0) FindLocationsOfTiles();
        SpawnEnemies();

    }



    private void SpawnEnemies()
    {
        //Debug.Log(availablePlaces.Count);

        for (int i = 0; i < availablePlaces.Count; i++)
        {
            int canSpawn = Random.Range(0, randomFactor);
            // spawn prefab at the vector's position which is at the availablePlaces location and add 0.5f units as the bottom left
            // of the CELL (square) is (0,0), the top right of the CELL (square) is (1,1) therefore, the middle is (0.5,0.5)

            if (canSpawn == 0)
            {
                //  print("triedspawn");
                // print(SceneManager.GetActiveScene().name);

                int enemyType = Random.Range(0, Spawns.Count);
                if (Spawns.Count != 0)
                {
                    var monster = Instantiate(Spawns[enemyType], new Vector3(availablePlaces[i].x + 0.5f, availablePlaces[i].y + 0.5f, availablePlaces[i].z), Quaternion.identity);
                    monster.SetActive(true);
                }//availablePlaces.Remove()
            }
            else
            {
                int x = Random.Range(0,70);
                if (x == 0)
                {
                    var instanceBarrel=Instantiate(barrel, new Vector3(availablePlaces[i].x + 0.5f, availablePlaces[i].y + 0.5f, availablePlaces[i].z), Quaternion.identity);
                    instanceBarrel.SetActive(true);
                }
            }

        }

    }
}
