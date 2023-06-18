using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Json : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public void SaveToJson()
    {
        var allStats = GameObject.FindGameObjectWithTag("GameController").GetComponent<WinLose>().GetStatsGather() ;

        string json= JsonUtility.ToJson(allStats);
        File.WriteAllText(Application.dataPath + "/LastPlayer.json", json);

    }

    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/Leaderboards.json");
        string jsonLastPlayer = File.ReadAllText(Application.dataPath + "/LastPlayer.json");

        LeaderBoards data = JsonUtility.FromJson<LeaderBoards>(json);
        StatsGather lastPlayer = JsonUtility.FromJson<StatsGather>(jsonLastPlayer);
        if (!CheckDuplicates(data, lastPlayer))
        { data.playersStats.Add(lastPlayer);
        string newLeaderboards = JsonUtility.ToJson(data);

        File.WriteAllText(Application.dataPath + "/Leaderboards.json", newLeaderboards); }

        SortLeaderboards(data);

        //  Debug.Log(data);
        // leaders.text = data.ToString();
        SetFields(data);
        
    }
    public void SetFields(LeaderBoards data)
    {
        int i = 0;
        foreach (var entry in objects)
        {
            var textFields = entry.GetComponentsInChildren<TextMeshProUGUI>();
            if (i<data.playersStats.Count)
            {
                textFields[0].text = data.playersStats[i].playerName;
                Debug.Log(textFields[0].text);
                textFields[1].text = data.playersStats[i].currClass.ToString();
                textFields[2].text = data.playersStats[i].floor.ToString();
                textFields[3].text = data.playersStats[i].level.ToString();
                textFields[4].text = data.playersStats[i].gold.ToString();
            }
            else for(int j=0;j<5;j++)
                {
                    textFields[j].text = null;
                }
            i++;
        }
    }
    
    public bool CheckDuplicates(LeaderBoards  dataSet, StatsGather lastPlayer)
    {
        foreach (var player in dataSet.playersStats)
        {   
            if (player.gold==lastPlayer.gold
                && player.level==lastPlayer.level
                && player.playerName==lastPlayer.playerName 
                && player.currClass==lastPlayer.currClass 
                && player.floor==lastPlayer.floor)
            {
            //    Debug.Log("FOUND INTRUDER");
                return true;
            }
        }
        return false;
    }
    public void SortLeaderboards(LeaderBoards dataSet)
    {
        for (int i = 0; i<dataSet.playersStats.Count; i++)
        {
            for (int j =i+1;j< dataSet.playersStats.Count;j++)
            {
                if (dataSet.playersStats[j].floor > dataSet.playersStats[i].floor)
                {
                    var bubble = dataSet.playersStats[j];

                    dataSet.playersStats[j] = dataSet.playersStats[i];
                    dataSet.playersStats[i]=bubble;


                }
                else if (dataSet.playersStats[j].floor== dataSet.playersStats[i].floor)
                {
                    if (dataSet.playersStats[j].level > dataSet.playersStats[i].level)
                    {
                        var bubble = dataSet.playersStats[j];

                        dataSet.playersStats[j] = dataSet.playersStats[i];
                        dataSet.playersStats[i] = bubble;

                    }
                }
            }

        }
    }
}
