using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomSpawner : MonoBehaviour
{
    public List<GameObject> Spawns = new List<GameObject>();
    public Tilemap walls;
    public Tilemap walls2;

    private int width = 10;
    private int length = 10;
    private int height = 1;
    [SerializeField]
    private int step = 3;
    [SerializeField]
    private int randomFactor = 3;

    // Start is called before the first frame update
    void Start()
    {
        walls = this.transform.parent.GetChild(0).GetComponent<Tilemap>();
        var tilemapSize = this.GetComponent<Tilemap>().size;
        width = tilemapSize.x;
        length= tilemapSize.y;
        if (length > width)
            width ^= length ^= width ^= length;
        for (int h = 0; h < length / step; h++)
        {
            for (int i = 0; i < width / step; i++)
            {
                int canSpawn = Random.Range(0, randomFactor);

                if (canSpawn == 0)
                {
                   int pos=Random.Range(0,Spawns.Count);
                    Vector3Int position = new Vector3Int((
                      (int)  ((i * step) + transform.position.x) - (width / 2)),
                       (int) transform.position.y +Random.Range(-(height / 2),
                        1));
                   // if (walls.HasTile((Vector3Int)position)==false)
                    if(walls.GetColliderType(position)==Tile.ColliderType.None && walls2.GetColliderType(position) == Tile.ColliderType.None && this.GetComponent<Tilemap>().HasTile(position)==true)
                    {
                        Instantiate(Spawns[pos], position, Quaternion.identity);
                    }
                   
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
