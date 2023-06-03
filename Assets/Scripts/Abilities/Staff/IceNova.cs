using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class IceNova : Ability
{
    float tickRate = 0.25f;
    float baseDamage = 1f;
    public GameObject spriteInstance;
    float ap;

    float radius = 3.1541f;
    // Start is called before the first frame update

    public override void Activate(GameObject parent)
    {
        ap = parent.GetComponent<StatsHolder>().getCurrStats().GetStatValue(StatType.ap);
        float totalDamage = baseDamage + ap *0.25f;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        spriteInstance = Instantiate(sprite);

        spriteInstance.GetComponent<IceNovaDamage>().SetDamage(totalDamage);
        spriteInstance.GetComponent<IceNovaDamage>().SetTickRate(tickRate);
        spriteInstance.GetComponent<IceNovaDamage>().setPlayer(parent);


        spriteInstance.GetComponent<Timer>().timeRemaining = tickRate;

        spriteInstance.GetComponent<Timer>().StartTimer();
        spriteInstance.transform.localScale = new Vector3( radius,radius,1);


        spriteInstance.transform.position = parent.transform.position;
        Destroy(spriteInstance,activeTime);
    }
    
    public override void BeginCooldown(GameObject gameObject)
    {
        Destroy(spriteInstance);
    }


}
