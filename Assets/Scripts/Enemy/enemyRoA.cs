using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRoA : MonoBehaviour
{

    private float damage = 1f;
    private bool canDamage = false;
    private float tickRate = 1.5f;
    private void FixedUpdate()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        canDamage = this.GetComponent<Timer>().consumeTrigger;

        if (canDamage)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0, 250, 0);
            this.GetComponent<Timer>().consumeTrigger = false;
            this.GetComponent<Timer>().timeRemaining = tickRate;
            this.GetComponent<Timer>().StartTimer();
        }

    }
    public void StartDamage()
    {
        
        this.GetComponent<Timer>().consumeTrigger = false;
        this.GetComponent<Timer>().timeRemaining = tickRate;
        this.GetComponent<Timer>().StartTimer();
        
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider == null) return;
        if (collider.tag == "Player" && canDamage )
        {
            Debug.Log("PlayerHit");
            collider.GetComponent<PlayerHealth>().TakeDamage(damage);


        }
    }
    public void SetDamage(float value)
    {
        damage = value;
    }
    public void SetTickRate(float tickrate)
    {
        this.tickRate = tickrate;
    }
}
