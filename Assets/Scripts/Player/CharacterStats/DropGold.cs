using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGold : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float goldValue = 1f;
    int maxGold = 10;
    int minGold = 1;
    public void SetMaxGold(int maxGold) { this.maxGold = maxGold; }
    public void SetMinGold(int minGold) { this.minGold = minGold; }

    private void OnEnable()
    {
       player = GameObject.FindGameObjectWithTag("Player");
        RollGold();
    }
    public void RollGold()
    {
        goldValue = (int) Random.Range(minGold, maxGold);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision!= null)
        {
            if(player != null)
            {
                if (collision.gameObject.tag == "Player")
                {
                    player.GetComponent<StatsHolder>().gainGold(goldValue);
                    Destroy(this.gameObject);
                }
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }
}
