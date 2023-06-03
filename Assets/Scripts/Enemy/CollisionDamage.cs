using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public GameObject player;
    private PlayerHealth playerHealth;
    private Invulnerability invulnerability;
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth=player.GetComponent<PlayerHealth>();
        invulnerability=player.GetComponent<Invulnerability>();

    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision!=null)
        if(collision.gameObject)
        if(collision.gameObject.tag!=null)
        if (collision.gameObject.tag == "Player"
            && invulnerability.invulnerabilityTime == 0)
        {

            playerHealth.TakeDamage(damage);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && invulnerability.invulnerabilityTime==0)
        {
            TryToDamage( damage);
        }
    }
    public void TryToDamage(int damage)
    {
        playerHealth.TakeDamage(damage);

    }
}
