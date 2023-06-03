using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  
    // Start is called before the first frame update
    public float health;

    public float maxHealth = 5;
    private GameObject player;
    [SerializeField]
    private float experience;
    [SerializeField]
    private GameObject goldPrefab;
    [SerializeField]
    private float goldChance=35f;
    private void Start()
    {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public void TakeDamage(float damage)
    {

            health -= damage;
        if (health <= 0 && player!=null)
        {
            player.GetComponent<StatsHolder>().increaseExp(experience);
            float dice = UnityEngine.Random.Range(0,100);
            if (dice<=goldChance)
            {
                GameObject goldCoin = Instantiate(goldPrefab);
                goldCoin.GetComponent<DropGold>().SetMaxGold((int)goldChance/10*2+3);
                goldCoin.GetComponent<DropGold>().SetMinGold((int) Mathf.Max((goldChance / 10) / 2,1));
                goldCoin.GetComponent<DropGold>().RollGold();
                goldCoin.transform.position = transform.position;
            }

            Destroy(gameObject);
        }
    }
    public bool isFull()
    {
        return health == maxHealth;
    }
    public void TakeHeal(float healValue)
    {
        health += healValue;
        if (health >= maxHealth) health = maxHealth;
    }
}

