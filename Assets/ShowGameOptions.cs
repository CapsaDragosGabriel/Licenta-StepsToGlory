using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameOptions : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        this.GetComponent<Canvas>().enabled = false;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {

            this.GetComponent<Canvas>().enabled = !this.GetComponent<Canvas>().enabled;
           
                var gameMenu=GetComponentInChildren<OptionsMenu>();
                gameMenu.SaveSettingsJson();
            
        }
    }
}
