using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ImbueAttacks : Ability
{
    float tickRate = 0.25f;
    float baseDamage = 1f;
    public GameObject spriteInstance;
    float ap;
    float currAd = 0f;
    float radius = 3.1541f;
    StatsHolder statsHolder;
    // Start is called before the first frame update
    public override void Activate(GameObject parent)
    {
        statsHolder = parent.GetComponent<StatsHolder>();
       
        ap = statsHolder.getCurrStats().GetStatValue(StatType.ap);
        currAd= statsHolder.getCurrStats().GetStatValue(StatType.ad);

        statsHolder.getCurrStats().UpgradeStat(StatType.ad,  ap * 0.5f);
 
        this.cooldownTime =Mathf.Max(this.baseCooldown - ap*0.2f,1) ;
    }

    public override void BeginCooldown(GameObject gameObject)
    {
        statsHolder.getCurrStats().DowngradeStat(StatType.ad,  ap * 0.5f );


    }


}
