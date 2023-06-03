using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public float maxHealth ;
    private GameObject player;
    private Invulnerability invulnerability;
    private HealthBar healthBar;
    private float lastEndurance=0f;
    private GameObject gameController;

    public bool isFull()
    {
        return health == maxHealth;
    }
    private void Start()
    {
        while (this == null) ;
        

        maxHealth = this.GetComponent<StatsHolder>().getCurrStats().GetStatValue(StatType.health);
        health = maxHealth;
        var go = GameObject.FindGameObjectWithTag("Healthbar");
        if (go != null)
        {
            healthBar = GameObject.FindGameObjectWithTag("Healthbar").GetComponent<HealthBar>();
        }
        gameController = GameObject.FindGameObjectWithTag("GameController");

        healthBar.SetMaxHealth(health);
        healthBar.SetHealth(health);

       
        this.gameObject.layer = 0;
        invulnerability= this.GetComponent<Invulnerability>();
    }
    private void Update()
    {
       
        float currEndurance = this.GetComponent<StatsHolder>().getCurrStats().GetStatValue(StatType.endurance);
        if (lastEndurance != currEndurance)
        {
            maxHealth += (currEndurance - lastEndurance);
            
            if (health > maxHealth)
                health = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(health);

            lastEndurance = currEndurance;
        }


    }
    public void TakeDamage(float damage)
    {

        if (gameObject.tag == "Player" && invulnerability.invulnerabilityTime == 0 && health>0)
        { health -= damage;
            healthBar.SetHealth(health);

            invulnerability.StartCoroutine("GetInvulnerable", invulnerability.defaultInvulnerability);
        }
        if (health<=0 )
        {
         
            Destroy(player);
            gameController.GetComponent<WinLose>().LoseLevel();
        }
    }
    public void TakeHeal(float heal)
    {
        if (heal + health > maxHealth) health = maxHealth;
        else health += heal;
        healthBar.SetHealth(health);    
    }
}
