using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public float damage = 2;
    AudioSource impactSoundWall;
    AudioSource impactSoundEnemy;
    float vol;
    private void Start()
    {
        Physics2D.IgnoreLayerCollision(0, 4, true);

        Physics2D.IgnoreLayerCollision(4, 4, true);
        impactSoundWall = GetComponents<AudioSource>()[0];
        impactSoundEnemy = GetComponents<AudioSource>()[1];

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log(vol);

        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Untagged" && collision.gameObject.tag != "Wall")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            }

            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(impactSoundEnemy.clip, transform.position,vol);
            
            Destroy(gameObject);
            Destroy(effect, 1f);


        }
        else if (collision.gameObject.tag == "Wall")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(impactSoundWall.clip, transform.position, vol);


            Destroy(gameObject);
            Destroy(effect, 1f);

        }
        
        else if (collision.gameObject.tag == "Loot")
        {
            collision.gameObject.GetComponent<BarrelDestructable>().TakeDamage(1);
            AudioSource.PlayClipAtPoint(impactSoundWall.clip, transform.position, vol);

            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(effect, 1f);
        }
     
    }

    private void FixedUpdate()
    {
        if (impactSoundEnemy != null)
        {
            impactSoundEnemy.outputAudioMixerGroup.audioMixer.GetFloat("Volume", out vol);
            vol = Mathf.Min((vol + 60) / 8000, 1);
        }
        if (gameObject)
        {
            Destroy(gameObject,4f);
        }
    }
}
