using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 2f;
    public float baseDamage = 2f;

    public bool canDamage = false;
    public float resetTimer = 0f;
    private void FixedUpdate()
    {
       // if (Input.GetButtonDown("Fire1"))
        StartCoroutine(DamageTimer());   
    }
    IEnumerator DamageTimer()
    {
        canDamage= true;
        yield return new WaitForSeconds(0.08f);
        canDamage= false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Untagged" && canDamage)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                {
                    collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                }
            }
            else if (collision.gameObject.tag == "Loot")
            { 
                    collision.gameObject.GetComponent<BarrelDestructable>().TakeDamage(2);

            }


        }
    }


}
