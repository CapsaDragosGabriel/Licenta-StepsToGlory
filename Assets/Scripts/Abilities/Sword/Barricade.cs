using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Barricade : Ability
{
    public GameObject spriteInstance;
    float ap;
    StatsHolder statsHolder;
    float radius = 1 * 3.1541f;
    // Start is called before the first frame update

    public override void Activate(GameObject parent)
    {
        statsHolder=GameObject.FindGameObjectWithTag("Player").GetComponent<StatsHolder>();
        ap = statsHolder.getCurrStats().GetStatValue(StatType.ap);
        spriteInstance = Instantiate(sprite);

        spriteInstance.GetComponent<BarricadeSpell>().setPlayer(parent);



        spriteInstance.transform.localScale = new Vector3(radius, radius, 1);

        this.cooldownTime = Mathf.Max(this.baseCooldown - ap * 0.2f, 1);

        spriteInstance.transform.position = parent.transform.position;
        Destroy(spriteInstance, activeTime);
    }

    public override void BeginCooldown(GameObject gameObject)
    {

        Destroy(spriteInstance);
    }

}
