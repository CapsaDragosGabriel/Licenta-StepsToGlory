using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBuy : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject player;
    private StatsHolder statsHolder;
    private Stats currStats;
    private GameObject gameController;
    public float price;
    private int healthValue;
    public bool isTouching = false;
    public float cooldown = 0.5f;
    public bool okCooldown = true;

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        statsHolder=player.GetComponent<StatsHolder>();
        currStats = statsHolder.getCurrStats();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        healthValue = 1;
        price = 4 * gameController.GetComponent<WinLose>().getNextIndex()+3 ;
        GetComponentInChildren<TextMeshProUGUI>().text = price.ToString();
        if (price > statsHolder.getGold()) GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 0, 0);
        okCooldown = true;
    }
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isTouching = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isTouching = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouching = false;
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isTouching = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        isTouching = true;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouching = false;

    }
   */
    // Update is called once per frame
    void FixedUpdate()
    {
        if(isTouching)
        {
            if (Input.GetKey(KeyCode.X) &&okCooldown) {
               if (statsHolder.getGold() >= price && !player.GetComponent<PlayerHealth>().isFull())
                {
                    okCooldown = false;
                    StartCoroutine(countCooldown());
                    player.GetComponent<PlayerHealth>().TakeHeal(healthValue);
                    statsHolder.spendGold(price); 
                    price += (int)price / 2;

                 }
               
            }
        }
        GetComponentInChildren<TextMeshProUGUI>().text = price.ToString();
        if (price > statsHolder.getGold()) GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 0, 0);

    }
    IEnumerator countCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        okCooldown = true;
    }
}
