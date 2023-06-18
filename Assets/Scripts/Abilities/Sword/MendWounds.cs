using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class MendWounds : Ability
{
    public GameObject spriteInstance;
    float ap;
    float currEnd = 0f;
    
    StatsHolder statsHolder;
    // Start is called before the first frame update
    public override void Activate(GameObject parent)
    {
        statsHolder = parent.GetComponent<StatsHolder>();

        ap = statsHolder.getCurrStats().GetStatValue(StatType.ap);
        currEnd = statsHolder.getCurrStats().GetStatValue(StatType.ad);

        parent.GetComponent<PlayerHealth>().TakeHeal((int) 1+ap/5 + currEnd/5 );

        this.cooldownTime = Mathf.Max(this.baseCooldown - ap * 0.2f, 1);
    }

    public override void BeginCooldown(GameObject gameObject)
    {


    }
}
