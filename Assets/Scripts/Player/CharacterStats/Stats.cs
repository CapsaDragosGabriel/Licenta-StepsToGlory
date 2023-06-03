using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu (menuName="Stats")]
public class Stats : ScriptableObject
{
    public List<StatInfo> statInfo= new List<StatInfo>();
    

    public Stats Clone()
    {
        Stats statsclone = new Stats();
        foreach (var e in statInfo)
        {
            statsclone.statInfo.Add(e);

        }
        return statsclone;
    }
    public void UpgradeStat(StatType stat,float value=1f)
    {
        foreach (var s in statInfo)
        {
            if (s.statType == stat)
            {
                s.statValue +=value;
                return;
            }
        }
        Debug.LogError("Wrong stat type");

    }

    public void DowngradeStat(StatType stat, float value = 1f)
    {
        foreach (var s in statInfo)
        {
            if (s.statType == stat)
            {
                if (s.statValue-value>=0f)
                s.statValue -= value;
                else s.statValue= 0f;
                return;
            }
        }
        Debug.LogError("Wrong stat type");

    }


    public void SetStat(StatType stat, float value = 1f)
    {
        foreach (var s in statInfo)
        {
            if (s.statType == stat)
            {
                s.statValue = value;
                return;
            }
        }
        Debug.LogError("Wrong stat type");

    }

    public float GetStatValue(StatType stat)
    {

        foreach (var s in statInfo)
        {
            if (s.statType == stat)
            {
                return s.statValue;
            }
        }
        Debug.LogError("Wrong stat type");
        return 0f;
    }
}
