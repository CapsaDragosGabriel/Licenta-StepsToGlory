using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abandon : MonoBehaviour
{
    // Start is called before the first frame update
    public void AbandonRun()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Json>().SaveToJson();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Json>().LoadFromJson();

        GameObject.FindGameObjectWithTag("GameController").GetComponent<WinLose>().LoseLevel();

    }
}
