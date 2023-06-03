using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CheckAoeCollision : Ability
{
    float tickRate = 2f;
    float baseDamage = 1f;
    public GameObject spriteInstance;
    float ap;
    //float radius = 3.1541f;
    // Start is called before the first frame update

    public override void Activate(GameObject parent)
    {
        ap = parent.GetComponent<StatsHolder>().getCurrStats().GetStatValue(StatType.ap);

        float totalDamage = baseDamage + ap * 0.25f;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        GameObject center = GameObject.FindGameObjectWithTag("Center");


        spriteInstance = Instantiate(sprite);
        if(center!=null)
        spriteInstance.GetComponent<Rigidbody2D>().rotation = center.GetComponent<Rigidbody2D>().rotation;

        spriteInstance.GetComponent<PeriodicDamageCollider>().SetDamage(totalDamage);
        spriteInstance.GetComponent<PeriodicDamageCollider>().SetTickRate(tickRate);
        //spriteInstance.transform.localScale = new Vector3(radius, radius, 1);
        spriteInstance.transform.position = mousePos;
        Destroy(spriteInstance,activeTime);

    }

    public override void BeginCooldown(GameObject gameObject)
    {
    }

 
}
