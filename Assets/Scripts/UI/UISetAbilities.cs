using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UISetAbilities : MonoBehaviour
{
    public List<Image> icons = new List<Image>();
    public List<GameObject> cdIcons = new List<GameObject>();

    private GameObject player;
    private List<AbilityHolder> holders= new List<AbilityHolder>();

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            for (int i = 0; i < 4; i++)
                holders.Add(player.GetComponents<AbilityHolder>()[i]);
            this.transform.localScale = Vector3.one;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        int offset = 20;
        for (int i =0; i < 4; i++)
        {
            GameObject target = icons[i].transform.GetChild(0).gameObject;
            if (holders[i].ability)
            {
                target.GetComponent<Image>().enabled = true;

                target.GetComponent<Image>().sprite = holders[i].ability.sprite.GetComponent<SpriteRenderer>().sprite;
                target.GetComponent<Image>().color = holders[i].ability.sprite.GetComponent<SpriteRenderer>().color;

                if (holders[i].getCooldownTime()>0)
                {
                    var cdDisplay = cdIcons[i];
                    cdDisplay.SetActive(true);
                    Debug.Log(holders[i].getCooldownTime() > 0);
                   

                    var text = cdDisplay.GetComponentInChildren<TextMeshProUGUI>();
                    string timer = ((int)holders[i].getCooldownTime()).ToString();
                    
                    text.text = timer;

                }
                else
                {
                    var cdDisplay = cdIcons[i];
                    cdDisplay.SetActive(false);
               
                }

            }
            else
            {
                target.GetComponent<Image>().enabled = false;

                var cdDisplay = cdIcons[i];
                cdDisplay.SetActive(false);
            }
            GameObject targetText = icons[i].transform.GetChild(1).gameObject;
            string dummyText = holders[i].key.ToString();
            if (dummyText.Contains("Alpha"))
            {
                dummyText=dummyText.Replace("Alpha", "");
            }
            targetText.GetComponent<TMPro.TextMeshProUGUI>().text = dummyText;

            RectTransform rectTransf = icons[i].GetComponent<RectTransform>();
            rectTransf.anchoredPosition = new Vector2(100f * i+offset, 15f);

            RectTransform rectTransfCd = cdIcons[i].GetComponent<RectTransform>();
            rectTransfCd.anchoredPosition = new Vector2(0,0);

            RectTransform rectTransfCdText = cdIcons[i].GetComponentsInChildren<RectTransform>()[1];
            rectTransfCdText.anchoredPosition = new Vector2(0, 0);

        }
    }
}
