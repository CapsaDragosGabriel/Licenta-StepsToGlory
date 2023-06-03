using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImportSprite : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private AbilityHolder holder;
    Ability ability;
    GameObject getSprite;
    Sprite newSprite;
    void Start()
    {
        holder=player.GetComponents<AbilityHolder>()[0];
        ability = holder.ability;
        getSprite = ability.sprite;
        newSprite = getSprite.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<Image>().sprite = newSprite;
    }

    // Update is called once per frame
    void Update()
    {
        ability = holder.ability;
        getSprite = ability.sprite;
        newSprite=getSprite.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<Image>().sprite = newSprite;
    }

}
