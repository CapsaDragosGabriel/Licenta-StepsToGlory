using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public Ability ability;
    public bool isPickup = false;
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isPickup = true;
    }
    public void FixedUpdate()
    {
        if (isPickup) {}
    }
}
