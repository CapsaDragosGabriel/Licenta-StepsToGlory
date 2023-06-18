using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class Whirlwind : Ability
{
    float tickRate = 0.20f;
    float baseDamage = 1f;
    public GameObject spriteInstance;
    float ap;
    float currMovespeed = 0;
    float radius =  3.1541f;
    // Start is called before the first frame update

    public override void Activate(GameObject parent)
    {
        ap = parent.GetComponent<StatsHolder>().getCurrStats().GetStatValue(StatType.ap);
        float totalDamage = baseDamage + ap * 0.25f;
        spriteInstance = Instantiate(sprite);

        spriteInstance.GetComponent<WhirlwindPeriodic>().SetDamage(totalDamage);
        spriteInstance.GetComponent<WhirlwindPeriodic>().SetTickRate(tickRate);
        spriteInstance.GetComponent<WhirlwindPeriodic>().setPlayer(parent);

        currMovespeed = parent.GetComponent<PlayerMovement>().moveSpeed;
        parent.GetComponent<PlayerMovement>().moveSpeed += 1;

        parent.GetComponent<PlayerMovement>().moveSpeed += ap / 5;
        spriteInstance.GetComponent<Timer>().timeRemaining = tickRate;

        spriteInstance.GetComponent<Timer>().StartTimer();

        this.cooldownTime = Mathf.Max(this.baseCooldown - ap * 0.2f, 1);

        spriteInstance.transform.position = parent.transform.position;
        Destroy(spriteInstance, activeTime);
    }

    public override void BeginCooldown(GameObject gameObject)
    {
        gameObject.GetComponent<PlayerMovement>().moveSpeed = currMovespeed;
        Destroy(spriteInstance);
    }


}
