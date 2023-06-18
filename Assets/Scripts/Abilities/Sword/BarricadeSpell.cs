using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeSpell : MonoBehaviour
{
    private GameObject followPlayer = null;


    public void setPlayer(GameObject player)
    {
        followPlayer = player;
    }
    private void Update()
    {
        if (followPlayer != null)
        {
            // followPlayer.GetComponent<Shooting>().setShoot(false);
            this.transform.position = followPlayer.transform.position;
        }
    }
}
