using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public float damage = 1;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(1, 1, true);
        Physics2D.IgnoreLayerCollision(3, 1, true);
        Physics2D.IgnoreLayerCollision(3, 4, true);
        Physics2D.IgnoreLayerCollision(3, 3, true);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
     
            if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag!="Untagged")
            {
                if (collision.gameObject.tag == "Player")
                {
                if (collision.gameObject.GetComponent<PlayerHealth>()!=null)
                    collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                }

                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(effect, 1f);

            }
       
    }

    private void FixedUpdate()
    {
       try{ 
            Destroy(this, 4f); 
        }
        catch
        {

        }

    }
}
