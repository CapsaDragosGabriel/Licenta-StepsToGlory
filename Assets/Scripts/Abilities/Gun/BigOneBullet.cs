using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigOneBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public float damage = 2;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(0, 4, true);

        Physics2D.IgnoreLayerCollision(4, 4, true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Untagged")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            }




        }

    }

    private void FixedUpdate()
    {
        if (gameObject)
        {
            Destroy(gameObject, 4f);
        }
    }
}
