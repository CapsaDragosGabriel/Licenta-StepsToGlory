using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Spearshot : Ability
{
    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject spearPrefab;
    public float bulletForce = 30f;
    // Update is called once per frame
    float ap;
    float ad;

    StatsHolder statsHolder;
    private Stats playerStats;
 
    

    public override void Activate(GameObject parent)
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsHolder>().getCurrStats();
        firePoint = GameObject.FindGameObjectWithTag("FirePoint").transform;

        statsHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsHolder>();
        ap = statsHolder.getCurrStats().GetStatValue(StatType.ap);
        ad= statsHolder.getCurrStats().GetStatValue(StatType.ad);
        this.cooldownTime = Mathf.Max(this.baseCooldown - ap * 0.2f - ad * 0.3f, 2);

        GameObject spear = Instantiate(spearPrefab, firePoint.position, firePoint.rotation);
        spear.GetComponent<Spear>().damage += Mathf.Pow(playerStats.GetStatValue(StatType.ad), 2) / Mathf.Pow(3, 2);
        spear.transform.localScale= Vector3.one;
        Rigidbody2D rb = spear.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(spear,0.5f+ap/10);
    }


  
}
