using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ShieldCharge : Ability
{
    // Start is called before the first frame update
    public GameObject spriteInstance;
    float ap;
    StatsHolder statsHolder;
    float radius = 1 * 3.1541f;
    bool stopDash = false;
    public override void Activate(GameObject parent)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        statsHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsHolder>();
        ap = statsHolder.getCurrStats().GetStatValue(StatType.ap);


        parent.GetComponent<PlayerMovement>().forcedDash(mousePos);

        this.cooldownTime = Mathf.Max(this.baseCooldown - ap * 0.2f, 1);

     
    }
   
    public override void BeginCooldown(GameObject gameObject)
    {

    }
}
