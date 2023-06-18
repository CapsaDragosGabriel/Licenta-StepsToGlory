using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class FanTheHammer : Ability
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
        statsHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsHolder>();
        ap = statsHolder.getCurrStats().GetStatValue(StatType.ap);



        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsHolder>().getCurrStats();
        firePoint = GameObject.FindGameObjectWithTag("FirePoint").transform;

        statsHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsHolder>();
        ap = statsHolder.getCurrStats().GetStatValue(StatType.ap);
        ad = statsHolder.getCurrStats().GetStatValue(StatType.ad);
        this.cooldownTime = Mathf.Max(this.baseCooldown - ap * 0.2f - ad * 0.3f, 2);



        GameObject[] bullets = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
         
            

            bullets[i] = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullets[i].GetComponent<Bullet>().damage += (Mathf.Pow(playerStats.GetStatValue(StatType.ad), 2) + Mathf.Pow(playerStats.GetStatValue(StatType.ap), 2)) / Mathf.Pow(3, 2);
            float minSpread = -0.15f;
            float maxSpread = 0.15f;

            
            Rigidbody2D rb = bullets[i].GetComponent<Rigidbody2D>();

            Vector2 shootDirection = (Vector2)firePoint.up + new Vector2(Random.Range(minSpread, maxSpread), Random.Range(minSpread, maxSpread));
            //Vector2 shootDirection = firePoint.up * bulletForce;

            rb.AddForce(shootDirection, ForceMode2D.Impulse);

            //rb.AddForce(shootDirection, ForceMode2D.Impulse);
            rb.velocity = rb.velocity.normalized * bulletForce;
            //setat rotatia sagetii

            Vector2 lookDir = shootDirection;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg ;

            rb.rotation = angle;
            rb.freezeRotation = true;

           
                Destroy(bullets[i], 0.5f);
        }
    }
    IEnumerator loadShot()
    {
        var startTime = Time.time;

        while (Time.time < startTime + activeTime)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public override void BeginCooldown(GameObject gameObject)
    {

    }
}
