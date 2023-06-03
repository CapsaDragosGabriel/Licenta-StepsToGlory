using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatsHolder : MonoBehaviour 
{
    [SerializeField]
    private Stats baseStats;

    private Stats currStats  ;
    [SerializeField]

    private Classes characterClass=Classes.Sorcerer;
    private int level=0;
    [SerializeField]
    private float experience = 0;
    [SerializeField]
    private float nextLvlExp = 2;
    private int points = 3;
    [SerializeField]
    private float gold = 0f;

    public Classes getClass() { return characterClass; }
    public float getGold() { return gold; }
    public void setGold(float gold) { this.gold = gold; }
    public void gainGold(float gold) { this.gold += gold;}
    public void spendGold(float gold) {
        if (this.gold>=gold) { this.gold -= gold; }
    }

    public int getPoints() {return points; }
    public int getLevel() { return level; }
       
    public float getExperience() { return experience; }
    public void increaseExp(float value)
    {
        experience += value;
        while (experience > nextLvlExp)
        {
            level += 1;
            experience -=nextLvlExp;
            nextLvlExp *= 1.75f;
            points += 1;
        }
    }
    public Stats getCurrStats() { return currStats; }
    public void updateStat(StatType statType)
    {
        if (currStats != null)
        {
            currStats.UpgradeStat(statType);
        }
    }
    public void incrEndurance() { if (points > 0) { currStats.UpgradeStat(StatType.endurance); points--; } }
    public void decrEndurance() {
        if (currStats.GetStatValue(StatType.endurance) > 0)
        {
            currStats.UpgradeStat(StatType.endurance, -1);
            points++;
        }
     }

    public void incrAd()
    {
        if (points > 0) { currStats.UpgradeStat(StatType.ad); points--; }
    }
    public void decrAd()
    {
        if (currStats.GetStatValue(StatType.ad) > 0)
        {
            currStats.UpgradeStat(StatType.ad, -1);
            points++;
        }
    }
    public void incrAp()
    {
        if (points > 0) { currStats.UpgradeStat(StatType.ap); points--; }
    }
    public void decrAp()
    {
        if (currStats.GetStatValue(StatType.ap) > 0)
        {
            currStats.UpgradeStat(StatType.ap, -1);
            points++;
        }
    }

    public void incrCd() => currStats.UpgradeStat(StatType.cd);

    public void decrCd() => currStats.UpgradeStat(StatType.cd);
    /*
    public void UpgradeCurrentStat(StatType statType,float value)
    {
       currStats.UpgradeStat(statType,value);
    }
    public void DowngradeCurrentStat(StatType statType, float value)
    {
        if (currStats.GetStatValue(statType) - value > 0)
        {
            currStats.UpgradeStat(statType, -value);
        }
        else currStats.SetStat(statType, 0);
    }*/
    public void Awake()
    {
        currStats=Instantiate(baseStats); 
    }

}
