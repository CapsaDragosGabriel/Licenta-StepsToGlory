using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public Ability ability;
    float cooldownTime;
    float activeTime;
    private GameObject player;
    [SerializeField]
    private bool isPickup = false;
    private List<KeyCode> abilityKeys =new List<KeyCode>();
    public bool canPickup = false;
    private AbilityHolder[] abilityHolders;
    enum AbilityState
    {
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;
    public float getCooldownTime() { return cooldownTime; }
    public KeyCode key =KeyCode.None;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
         abilityHolders= player.GetComponents<AbilityHolder>();
        for (int i=0; i<abilityHolders.Length; i++)
        {
            abilityKeys.Add(abilityHolders[i].key);
        }
        if (isPickup)
        {
            var currSprite = this.GetComponent<SpriteRenderer>();
            if (ability)
            {
                currSprite.sprite = ability.sprite.GetComponent<SpriteRenderer>().sprite;
                currSprite.color = ability.sprite.GetComponent<SpriteRenderer>().color;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player" && isPickup)
            canPickup = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag=="Player" && isPickup)
            canPickup = false;
    }

    public void forceStopAbility()
        {
        activeTime = 0;
        ability.BeginCooldown(gameObject);
        state = AbilityState.cooldown;
        cooldownTime = ability.cooldownTime;
        }

    // Update is called once per frame
    void Update()
    {
        if (canPickup)
        {
            for (int i = 0; i < abilityKeys.Count; i++)
            {
                if (Input.GetKeyDown(abilityKeys[i]))
                {
                    var currAbility = ability;
                    if (abilityHolders[i].ability)
                    {
                        ability = abilityHolders[i].ability;

                        abilityHolders[i].ability = currAbility;
                        var currSprite = this.GetComponent<SpriteRenderer>();
                        if (ability)
                        {
                            currSprite.sprite = ability.sprite.GetComponent<SpriteRenderer>().sprite;
                            currSprite.color = ability.sprite.GetComponent<SpriteRenderer>().color;
                        }
                    }
                    else
                    {
                        abilityHolders[i].ability = currAbility;
                        Destroy(this.gameObject);
                    }

                }
            }
            
        }
        else
            switch (state)
            {
                case AbilityState.ready:
                    if (Input.GetKeyDown(key))
                    {
                        if (ability)
                        { 
                        ability.Activate(gameObject);
                        state = AbilityState.active;
                        activeTime = ability.activeTime;
                        }
                    }
                    break;
                case AbilityState.active:
                    if (activeTime > 0)
                    {
                        activeTime -= Time.deltaTime;
                    }
                    else
                    {
                        ability.BeginCooldown(gameObject);
                        state = AbilityState.cooldown;
                        cooldownTime = ability.cooldownTime;
                    }
                    break;
                case AbilityState.cooldown:
                    if (cooldownTime > 0)
                    {
                        cooldownTime -= Time.deltaTime;
                    }
                    else
                    {
                        state = AbilityState.ready;
                    }
                    break;
            }
      
    }
}
