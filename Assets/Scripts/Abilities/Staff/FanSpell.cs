using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class FanSpell : Ability
{// Start is called before the first frame update
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float bulletForce = 30f;
    // Update is called once per frame
    float ap;
    float ad;

    StatsHolder statsHolder;
    private Stats playerStats;

    public override void Activate(GameObject parent)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        statsHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsHolder>();
        ap = statsHolder.getCurrStats().GetStatValue(StatType.ap);



        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsHolder>().getCurrStats();
        firePoint = GameObject.FindGameObjectWithTag("FirePoint").transform;

        statsHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsHolder>();
        ap = statsHolder.getCurrStats().GetStatValue(StatType.ap);
        ad = statsHolder.getCurrStats().GetStatValue(StatType.ad);
        this.cooldownTime = Mathf.Max(this.baseCooldown - ap * 0.2f - ad * 0.3f, 2);



        GameObject[] projectiles = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            int k;
            if (i < 2) k = -i;

            projectiles[i] = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectiles[i].GetComponent<FanSpellProjectile>().damage += (Mathf.Pow(playerStats.GetStatValue(StatType.ad), 2) + Mathf.Pow(playerStats.GetStatValue(StatType.ap), 2)) / Mathf.Pow(3, 2);
           // projectiles[i].transform.localScale = Vector3.one;
            float minSpread = -0.5f;
            float maxSpread = 0.5f;



            Rigidbody2D rb = projectiles[i].GetComponent<Rigidbody2D>();

            Vector2 shootDirection = (Vector2)firePoint.up + new Vector2(Random.Range(minSpread, maxSpread), Random.Range(minSpread, maxSpread));
            rb.AddForce(shootDirection, ForceMode2D.Impulse);

            //rb.AddForce(shootDirection, ForceMode2D.Impulse);
            rb.velocity = rb.velocity.normalized * bulletForce;
            //setat rotatia sagetii

            Vector2 lookDir = shootDirection;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            rb.rotation = angle;
            rb.freezeRotation = true;

            Destroy(projectiles[i], 0.7f+ap/8);


        }
    }

    public override void BeginCooldown(GameObject gameObject)
    {

    }
}
