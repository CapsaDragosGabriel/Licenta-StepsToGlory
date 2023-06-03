using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StatUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    public Stats playerStats;
    public StatsHolder statsHolder;

    [SerializeField]
    private GameObject charWindow;

    [SerializeField]
    private GameObject points;
    [SerializeField]
    private GameObject level;

    [SerializeField]
    private GameObject endField;
    


    [SerializeField]
    private GameObject ad;

    [SerializeField]
    private GameObject ap;


    [SerializeField]
    private GameObject gold;
    //[SerializeField]
    //private GameObject ap=null;

    public void incrEndurance()
    {
        if (statsHolder != null)
        { statsHolder.incrEndurance();
        }
    }
    public void decrEndurance()
    {
        if (statsHolder != null)
        {
            statsHolder.decrEndurance();
        }
    }

    public void incrAd()
    {
        if (statsHolder != null)
        {
            statsHolder.incrAd();
        }
    }
    public void decrAd()
    {
        if (statsHolder != null)
        {
            statsHolder.decrAd();
        }
    }
    public void incrAp()
    {
        if (statsHolder != null)
        {
            statsHolder.incrAp();
        }
    }
    public void decrAp()
    {
        if (statsHolder != null)
        {
            statsHolder.decrAp();
        }
    }
    void Update()
    {
        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerStats = player.GetComponent<StatsHolder>().getCurrStats();
            statsHolder= player.GetComponent<StatsHolder>();
            //minusEnd.GetComponent<Button>().
          //  Button minusEnd = GameObject.FindGameObjectWithTag("MinusStat").GetComponent<Button>();
           // minusEnd.onClick.RemoveAllListeners();

//            minusEnd.onClick.AddListener(() => this.incrEndurance()) ;
            // minusEnd.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { player.GetComponent<StatsHolder>().decrEndurance(); });
            // plusEnd.GetComponent<Button>().onClick.AddListener(delegate { statsHolder.incrEndurance(); });


        }
        if (charWindow.GetComponent<Canvas>().enabled)
        if (playerStats != null && player!=null )
        {
            ap.GetComponent<TextMeshProUGUI>().text = playerStats.GetStatValue(StatType.ap).ToString();
            ad.GetComponent<TextMeshProUGUI>().text = playerStats.GetStatValue(StatType.ad).ToString();
            level.GetComponent<TextMeshProUGUI>().text = player.GetComponent<StatsHolder>().getLevel().ToString();
            points.GetComponent<TextMeshProUGUI>().text= player.GetComponent<StatsHolder>().getPoints().ToString();
            endField.GetComponent<TextMeshProUGUI>().text = playerStats.GetStatValue(StatType.endurance).ToString();
            gold .GetComponent<TextMeshProUGUI>().text = player.GetComponent<StatsHolder>().getGold().ToString();

            }
    }
}
