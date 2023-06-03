using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHeal : MonoBehaviour
{
    [SerializeField]
    private float damage = 1f;
    private bool canDamage = false;
    private float tickRate = 1.5f;
    [SerializeField]
    private float healValue = 1f;

    private List<Collider2D> colliders = new List<Collider2D>();

    private void FixedUpdate()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(0, 250, 0);
        canDamage = this.GetComponent<Timer>().consumeTrigger;

        if (canDamage)
        {
            Heal();
            StartCoroutine(ChangeColors());
            this.GetComponent<Timer>().consumeTrigger = false;
            this.GetComponent<Timer>().timeRemaining = tickRate;
            this.GetComponent<Timer>().StartTimer();
        }

    }
    IEnumerator ChangeColors()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(250, 0, 0);
        yield return new WaitForSeconds(1.5f);
        this.GetComponent<SpriteRenderer>().color = new Color(0, 250, 0);

    }
    public void StartDamage()
    {

        this.GetComponent<Timer>().consumeTrigger = false;
        this.GetComponent<Timer>().timeRemaining = tickRate;
        this.GetComponent<Timer>().StartTimer();

    }
    private void Heal()
    {
        foreach(Collider2D collider in colliders)
        {
            if (collider == null) continue;

            if (collider.tag == "Player" )
            {
                Debug.Log("PlayerHit");
                collider.GetComponent<PlayerHealth>().TakeDamage(damage);


            }
            if (collider.tag == "Enemy")
            {
                Debug.Log("EnemyHealed");
                collider.GetComponent<EnemyHealth>().TakeHeal(healValue);


            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!colliders.Contains(collision)) colliders.Add(collision);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!colliders.Contains(collision)) colliders.Add(collision);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliders.Contains(collision)) colliders.Remove(collision);

    }
    public void SetDamage(float value)
    {
        damage = value;
    }
    public void SetHeal(float value)
    {
        healValue = value;
    }
    public void SetTickRate(float tickrate)
    {
        this.tickRate = tickrate;
    }
}
