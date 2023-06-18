using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelDestructable : MonoBehaviour
{
    public float life = 2f;
    public GameObject coinPrefab;
    public GameObject barrelTop;
    public GameObject barrelBottom;
    public void TakeDamage(float damage)
    {
        if (life>0)
        {
            life-= damage;
            if (life<=0)
            {
                float x = Random.Range(0, 5);
                if (x==0)
                Instantiate(coinPrefab,this.transform.position,Quaternion.identity);

               Destroy( Instantiate(barrelTop, this.transform.position, Quaternion.identity),0.8f);
                Destroy(Instantiate(barrelBottom, this.transform.position, Quaternion.identity),0.8f);

                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyProj" || collision.gameObject.tag == "PlayerProj") TakeDamage(1);
        else if (collision.gameObject.tag == "MeleeSwipe") TakeDamage(2);
     }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyProj" || collision.gameObject.tag == "PlayerProj") TakeDamage(1);
        else if (collision.gameObject.tag == "MeleeSwipe") TakeDamage(2);
    }

}
