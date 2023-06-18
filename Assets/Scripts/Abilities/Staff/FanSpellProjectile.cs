using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpellProjectile : MonoBehaviour
{
    public GameObject hitEffect;
    public float damage = 1;
    private void Start()
    {
        Physics2D.IgnoreLayerCollision(0, 4, true);

        Physics2D.IgnoreLayerCollision(4, 4, true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(effect, 1f);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Untagged")
        {
            if (collision.gameObject.tag == "Enemy" && collision.GetType() == typeof(BoxCollider2D))
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                
            }

        }
        else if (collision.gameObject.tag == "Wall")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(effect, 1f);
        }


    }

}
