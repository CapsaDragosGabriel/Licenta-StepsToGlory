using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using static UnityEditor.PlayerSettings;
using UnityEngine.SceneManagement;
public class spawner2 : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Spawns = new List<GameObject>();
    [SerializeField]
    private int randomFactor;
    private Tilemap walls;
    private Tilemap walls2;
    private Tilemap tileMap;
    [SerializeField]
    private List<Vector3> availablePlaces;
    bool spawned=false;

    void OnEnable()
    {
       if(!spawned)
            {
            //print(SceneManager.GetActiveScene().name);
            spawned = true;
            tileMap = GetComponent<Tilemap>();
            walls = GameObject.FindGameObjectsWithTag("Wall")[0].GetComponent<Tilemap>();
            walls2 = GameObject.FindGameObjectsWithTag("Wall")[1].GetComponent<Tilemap>();

            FindLocationsOfTiles();
            SpawnEnemies();
        }
            
       

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
            walls = GameObject.FindGameObjectsWithTag("Wall")[0].GetComponent<Tilemap>();
            walls2 = GameObject.FindGameObjectsWithTag("Wall")[1].GetComponent<Tilemap>();
            spawned = true;
            FindLocationsOfTiles();
            SpawnEnemies();
        }
    }
    public void ForceSpawn()
    {
        StartCoroutine("WaitBeforeSpawn");

    }
    private void FindLocationsOfTiles()
    {
        availablePlaces = new List<Vector3>(); // create a new list of vectors by doing...

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++) // scan from left to right for tiles
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++) // scan from bottom to top for tiles
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)tileMap.transform.position.y); // if you find a tile, record its position on the tile map grid
                Vector3 place = tileMap.CellToWorld(localPlace); // convert this tile map grid coords to local space coords
                if (tileMap.HasTile(localPlace)&&!walls.HasTile(localPlace)&& !walls2.HasTile(localPlace))
                {
                    //Tile at "place"
                    availablePlaces.Add(place);
                }
               
            }
        }
    }

 

    private void SpawnEnemies()
    {

        for (int i = 0; i < availablePlaces.Count; i++)
            {
            int canSpawn = Random.Range(0, randomFactor);

            // spawn prefab at the vector's position which is at the availablePlaces location and add 0.5f units as the bottom left
            // of the CELL (square) is (0,0), the top right of the CELL (square) is (1,1) therefore, the middle is (0.5,0.5)
            
            if (canSpawn == 0)
            {
              //  print("triedspawn");
               // print(SceneManager.GetActiveScene().name);

                int enemyType =  Random.Range(0, Spawns.Count);
                Instantiate(Spawns[enemyType], new Vector3(availablePlaces[i].x + 0.5f, availablePlaces[i].y + 0.5f, availablePlaces[i].z), Quaternion.identity);
                //availablePlaces.Remove()
            }

        }
       
    }
}
