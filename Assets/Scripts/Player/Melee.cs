using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject swipe;
    // Update is called once per frame
    private bool canAttack = true;
    [SerializeField]
    private float baseAS = 0.4f;

    private float attackSpeed;
    private float attackTimer = 0f;
    private Stats playerStats;
    public void setAttack(bool canAttack)
    {
        this.canAttack=canAttack;
    }
    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsHolder>().getCurrStats();
        swipe = GameObject.FindGameObjectWithTag("MeleeSwipe");
        swipe.GetComponent<PolygonCollider2D>().enabled = false;
        swipe.GetComponent<SpriteRenderer>().enabled = false;
        swipe.GetComponent<MeleeDamage>().resetTimer = 0.08f;
    }

    void Update()
    {
        swipe.GetComponent<MeleeDamage>().damage = swipe.GetComponent<MeleeDamage>().baseDamage+    Mathf.Pow(playerStats.GetStatValue(StatType.ad), 1.5f) / Mathf.Pow(3, 2);

        if (!canAttack) AttackingSpeed();

        if (Input.GetButtonDown("Fire1") && canAttack   )
        {
            Attack();
            canAttack = false;

        }

    }


    void Attack()
    {

        //swipe.GetComponent<MeleeDamage>().damage= Mathf.Pow(playerStats.GetStatValue(StatType.ad), 2.5f) / Mathf.Pow(3, 2);
        //GameObject swipeInstance = Instantiate(swipe);
        StartCoroutine(SwipeActive());
        attackSpeed = Mathf.Max(baseAS - playerStats.GetStatValue((StatType)StatType.ad) / 10, 0.1f);
    }

    IEnumerator SwipeActive()
    {
        swipe.GetComponent<PolygonCollider2D>().enabled = true;
        swipe.GetComponent<SpriteRenderer>().enabled = true;
        //swipe.SetActive(true);
        yield return new WaitForSeconds(0.08f);
        swipe.GetComponent<PolygonCollider2D>().enabled = false;
        swipe.GetComponent<SpriteRenderer>().enabled = false;
        //   D
        //swipe.SetActive(false);

    }
    private void AttackingSpeed()
    {
        if (attackTimer >= attackSpeed) { canAttack = true; attackTimer = 0; }
        else attackTimer += Time.deltaTime;
    }
}
