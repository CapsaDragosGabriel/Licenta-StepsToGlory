using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public float damage = 1;
    AudioSource impactSound;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(1, 1, true);
        Physics2D.IgnoreLayerCollision(3, 1, true);
        Physics2D.IgnoreLayerCollision(3, 4, true);
        Physics2D.IgnoreLayerCollision(3, 3, true);
        impactSound=GetComponent<AudioSource>();    

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
     
            if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "EnemyProj")
            {
                if (collision.gameObject.tag == "Player")
                {

                if (collision.gameObject.GetComponent<PlayerHealth>()!=null)
                    collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
               
                }
            float vol;
            if (impactSound != null)
            {
                impactSound.outputAudioMixerGroup.audioMixer.GetFloat("Volume", out vol);
                    AudioSource.PlayClipAtPoint(impactSound.clip, transform.position, Mathf.Min( (vol + 60) / 1000,1));
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
