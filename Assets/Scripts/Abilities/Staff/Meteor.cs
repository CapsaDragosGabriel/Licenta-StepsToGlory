using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Meteor : Ability
{
    float tickRate = 0.25f;
    [SerializeField]
    float baseDamage = 1.5f;
    public GameObject spriteInstance;
    float ap;
    float radius = 3.1541f;
    // Start is called before the first frame update

    public override void Activate(GameObject parent)
    {

        ap = parent.GetComponent<StatsHolder>().getCurrStats().GetStatValue(StatType.ap);
        float totalDamage = baseDamage + ap * 0.4f;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        // sprite.GetComponent<MeteorDamage>().SetDamage(damage);
        this.cooldownTime = Mathf.Max(this.baseCooldown - ap * 0.2f, 1);

        spriteInstance = Instantiate(sprite);

        spriteInstance.GetComponent<MeteorDamage>().SetDamage(totalDamage);
        spriteInstance.GetComponent<MeteorDamage>().SetTickRate(tickRate);


        spriteInstance.GetComponent<Timer>().timeRemaining = tickRate;
        activeTime = 2*tickRate;
        spriteInstance.GetComponent<Timer>().StartTimer();

        spriteInstance.transform.position = mousePos;
        Destroy(spriteInstance,activeTime);

    }

    public override void BeginCooldown(GameObject gameObject)
    {
        Destroy(spriteInstance);
    }

 
}
