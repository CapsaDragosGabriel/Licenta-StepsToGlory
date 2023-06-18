using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class TheBigOne : Ability
{// Start is called before the first frame update
    public Transform firePoint;
    public GameObject bulletPrefab;
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
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsHolder>().getCurrStats();
        firePoint = GameObject.FindGameObjectWithTag("FirePoint").transform;

        statsHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsHolder>();
        ap = statsHolder.getCurrStats().GetStatValue(StatType.ap);
        ad = statsHolder.getCurrStats().GetStatValue(StatType.ad);
        this.cooldownTime = Mathf.Max(this.baseCooldown - ap * 0.2f - ad * 0.3f, 2);





       
            GameObject  bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Spear>().damage += 2*(Mathf.Pow(playerStats.GetStatValue(StatType.ad), 2) + Mathf.Pow(playerStats.GetStatValue(StatType.ap), 2)) / Mathf.Pow(3, 2);
             bullet.transform.localScale = Vector2.one;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            //Vector2 shootDirection = (Vector2)firePoint.up + new Vector2(Random.Range(minSpread, maxSpread), Random.Range(minSpread, maxSpread));
            Vector2 shootDirection = firePoint.up * bulletForce;

            rb.AddForce(shootDirection, ForceMode2D.Impulse);

            rb.velocity = rb.velocity.normalized * bulletForce;

            Vector2 lookDir = shootDirection;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

            rb.rotation = angle;
            rb.freezeRotation = true;

         
            Destroy(bullet, 0.5f+ ap/10);
        
    }
  
    public override void BeginCooldown(GameObject gameObject)
    {

    }
}
