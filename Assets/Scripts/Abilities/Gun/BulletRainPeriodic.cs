using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRainPeriodic : MonoBehaviour
{
    // Start is called before the first frame update
    private float damage;
    float tickRate=0.25f;
    public bool canDamage = false;
    public StatusEffectData _data;

    private void FixedUpdate()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);

        canDamage = this.GetComponent<Timer>().consumeTrigger;
        if (canDamage)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(250,0,0);
            this.GetComponent<Timer>().consumeTrigger = false;
            this.GetComponent<Timer>().timeRemaining = tickRate;
            this.GetComponent<Timer>().StartTimer();
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        var effectable = collider.GetComponent<IEffecteable>();

        if (collider == null) return;
        if (collider.tag == "Enemy" && canDamage && collider.GetType() == typeof(BoxCollider2D) )
        {
            Debug.Log("TargetHit");
            collider.GetComponent<EnemyHealth>().TakeDamage((int)damage);
            if (effectable != null)

                effectable.ApplyEffect(_data);

        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider == null) return;

        var effectable = collider.GetComponent<IEffecteable>();
        if (collider.GetType() == typeof(BoxCollider2D) && collider.tag == "Enemy" && canDamage)
        {
            if (effectable!=null)
            effectable.ApplyEffect(_data);
            
            
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
