using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicDamageCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private float damage;
    float tickRate;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
       if (collider == null) return;
       if (collider.tag == "Enemy")
        {
            Debug.Log("TargetHit");
            StartCoroutine("DamageEnemies", collider);
             
        }
    }
   
    IEnumerator DamageEnemies(Collider2D collider)
    {
        collider.GetComponent<EnemyHealth>().TakeDamage((int)damage);
        yield return new WaitForSeconds(tickRate);
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
