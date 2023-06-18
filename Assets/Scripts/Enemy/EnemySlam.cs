using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlam : MonoBehaviour
{

    private float damage = 1f;
    private bool canDamage = false;
    private float tickRate = 0.75f;
    private void FixedUpdate()
    {
        var c = this.GetComponent<SpriteRenderer>().color;
        c.a = .20f;
        this.GetComponent<SpriteRenderer>().color = c;
        canDamage = this.GetComponent<Timer>().consumeTrigger;

        if (canDamage)
        {
            c.a = .80f;
            this.GetComponent<SpriteRenderer>().color = c;
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
        if (collider.tag == "Player" && canDamage)
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
