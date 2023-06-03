using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 2f;
    public bool canDamage = false;
    public float resetTimer = 0f;
    List<GameObject> enemies = new List<GameObject>();
    List<GameObject> damageables= new List<GameObject>();
    private void FixedUpdate()
    {
       // if (Input.GetButtonDown("Fire1"))
        StartCoroutine(DamageTimer());   
    }
    IEnumerator DamageTimer()
    {
        canDamage= true;
        yield return new WaitForSeconds(0.08f);
        enemies.Clear();
        damageables.Clear();
        canDamage= false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       
    }
    private void OnTriggerStay2D (Collider2D collision)
    {/*
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Untagged" && canDamage)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                {
                    collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                    if (collision.gameObject)
                        enemies.Add(collision.gameObject);
                }
            }
            

        }*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Untagged" && canDamage)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                {
                    collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                    if (collision.gameObject)
                        enemies.Add(collision.gameObject);
                }
            }


        }
    }


}
