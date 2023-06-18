using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private float damage;
    float tickRate=0.25f;
    public bool canDamage = false;
    public StatusEffectData _data;
    private int  b=255;
    private float a = 1;
    private void Start()
    {
       
    }
    private void Update()
    {
       
    }
    private void FixedUpdate()
    {
       StartCoroutine(changeColors());

        canDamage = this.GetComponent<Timer>().consumeTrigger;
        if (canDamage)
        {
            this.GetComponent<Timer>().consumeTrigger = false;
            this.GetComponent<Timer>().timeRemaining = 0;
            
        }
    }
    IEnumerator changeColors()
    {
       

            b = Mathf.Max(b - 30, 0);
            var c = new Color(0, 0, b);
            a -= .1f;
        c.a = a;
            this.GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(0.025f);
       

    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        var effectable = collider.GetComponent<IEffecteable>();

        if (collider == null) return;

         if (collider.GetType() == typeof(BoxCollider2D)&&collider.tag == "Enemy" && canDamage)
        {
            Debug.Log("TargetHit");
            collider.GetComponent<EnemyHealth>().TakeDamage((int)damage);

            if (effectable != null)
                effectable.ApplyEffect(_data);

        }
    }
   /* private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider == null) return;

        var effectable = collider.GetComponent<IEffecteable>();
        if ( collider.GetType() == typeof(BoxCollider2D) &&collider.tag=="Enemy")
        {
            if(effectable != null)
                effectable.ApplyEffect(_data);
            
            Debug.Log("TargetHit");
            collider.GetComponent<EnemyHealth>().TakeDamage(damage);
           // this.GetComponent<Timer>().consumeTrigger = false;
           // this.GetComponent<Timer>().timeRemaining = tickRate;
            //this.GetComponent<Timer>().StartTimer();

            
        }
    }
   */
    public void SetDamage(float value)
    {
        damage = value;
    }
    public void SetTickRate(float tickrate)
    {
        this.tickRate = tickrate;
    }
}
