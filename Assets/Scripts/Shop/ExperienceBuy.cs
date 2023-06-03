using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExperienceBuy : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private StatsHolder statsHolder;
    private Stats currStats;
    private GameObject gameController;
    public float price;
    private int expValue;
    public bool isTouching = false;

    public float cooldown = 0.5f;
    public bool okCooldown = true;
    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        statsHolder = player.GetComponent<StatsHolder>();
        currStats = statsHolder.getCurrStats();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        expValue = 2* gameController.GetComponent<WinLose>().getNextIndex() + 2;
        price = 7 * gameController.GetComponent<WinLose>().getNextIndex() + 5;
        GetComponentInChildren<TextMeshProUGUI>().text = price.ToString();
        okCooldown = true;
        if (price > statsHolder.getGold()) GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 0, 0);
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
    // Update is called once per frame
    IEnumerator countCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        okCooldown = true;
    }
    void FixedUpdate()
    {
        if (isTouching)
        {
            if (Input.GetKeyUp(KeyCode.X)&&okCooldown)
            {
                if (statsHolder.getGold() >= price)
                {
                    okCooldown = false;
                    StartCoroutine(countCooldown());
                    statsHolder.increaseExp(expValue);
                    statsHolder.spendGold(price);

                    price +=(int) price / 2;
                    GetComponentInChildren<TextMeshProUGUI>().text = price.ToString();
                    if (price > statsHolder.getGold()) GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 0, 0);
                }
               
            }
        }
        GetComponentInChildren<TextMeshProUGUI>().text = price.ToString();
        if (price > statsHolder.getGold()) GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 0, 0);

    }
}
