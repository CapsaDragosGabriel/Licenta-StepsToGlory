using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlwindPeriodic : MonoBehaviour
{
    // Start is called before the first frame update
    private float damage;
    float tickRate = 0.25f;
    public bool canDamage = false;
    private GameObject followPlayer = null;



    public void setPlayer(GameObject player)
    {
        followPlayer = player;
    }
    private void FixedUpdate()
    {
        transform.GetChild(0).transform.Rotate(0, 0, 5);

        if (followPlayer != null)
        {
            followPlayer.GetComponent<Melee>().setAttack(false);
            this.transform.position = followPlayer.transform.position;
        }
        canDamage = this.GetComponent<Timer>().consumeTrigger;
        if (canDamage)
        {
           // this.GetComponent<SpriteRenderer>().color = new Color(250, 0, 0);
            this.GetComponent<Timer>().consumeTrigger = false;
            this.GetComponent<Timer>().timeRemaining = tickRate;
            this.GetComponent<Timer>().StartTimer();
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider == null) return;
        if (collider.tag == "Enemy" && canDamage && collider.GetType() == typeof(BoxCollider2D))
        {
            Debug.Log("TargetHit");
            collider.GetComponent<EnemyHealth>().TakeDamage((int)damage);


        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider == null) return;
        if (collider.tag == "Enemy" && canDamage && collider.GetType() == typeof(BoxCollider2D))
        {
            Debug.Log("TargetHit");
            collider.GetComponent<EnemyHealth>().TakeDamage((int)damage);
            //this.GetComponent<Timer>().consumeTrigger = false;
            //this.GetComponent<Timer>().timeRemaining = tickRate;
            //this.GetComponent<Timer>().StartTimer();

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
