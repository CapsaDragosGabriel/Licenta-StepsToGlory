using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbilityBuy : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private StatsHolder statsHolder;
    private Stats currStats;
    private GameObject gameController;
    public float price;
    public bool bought = false;

    List<Ability> potentialAbilities= new List<Ability>();
    public bool isTouching = false;

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        statsHolder = player.GetComponent<StatsHolder>();
        currStats = statsHolder.getCurrStats();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        price = 7 * gameController.GetComponent<WinLose>().getNextIndex() + 15;

        GetComponentInChildren<TextMeshProUGUI>().text = price.ToString();
        if (price > statsHolder.getGold()) GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 0, 0);



        while (player == null) ;
        potentialAbilities = player.GetComponent<PotentialAbilities>().getAbilities(statsHolder.getClass());
        int abCount = potentialAbilities.Count;

        int index = Random.Range(0, abCount);
        if (index<abCount)
        GetComponentInChildren<AbilityHolder>().ability = potentialAbilities[index]; 

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
    void FixedUpdate()
    {
        if (isTouching)
        {
            if (Input.GetKeyUp(KeyCode.X))
            {
                if (statsHolder.getGold() >= price && !bought)
                {
                    bought = true;
                    statsHolder.spendGold(price);
                    GetComponent<SpriteRenderer>().enabled = false;
                    GetComponentsInChildren<CapsuleCollider2D>()[0].enabled = false;

                    GetComponentsInChildren<CapsuleCollider2D>()[1].enabled = true;
                  //  price += (int)price / 2;
                    GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                    if (price > statsHolder.getGold()) GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 0, 0);
                }

            }
        }
        GetComponentInChildren<TextMeshProUGUI>().text = price.ToString();
        if (price > statsHolder.getGold()) GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 0, 0);

    }
}
