using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisableEventSystem : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        this.GetComponent<StandaloneInputModule>().enabled = !this.GetComponent<StandaloneInputModule>().enabled;
        this.GetComponent<EventSystem>().enabled = !this.GetComponent<EventSystem>().enabled;
    }
    void Update()
    {
        var charWindowObj = GameObject.FindGameObjectWithTag("CharWindow");
        var settingsObj = GameObject.FindGameObjectWithTag("OptionsMenu");
        Canvas charWindow, settings;
        if (charWindowObj != null && settingsObj != null)
        {
             charWindow=charWindowObj.GetComponent<Canvas>();
            settings=settingsObj.GetComponent<Canvas>();
            if (charWindow.enabled || settings.enabled)
            {

                //this.GetComponent<StandaloneInputModule>().enabled = !this.GetComponent<StandaloneInputModule>().enabled;
                // this.GetComponent<EventSystem>().enabled = !this.GetComponent<EventSystem>().enabled;
                this.GetComponent<StandaloneInputModule>().enabled = true;
                this.GetComponent<EventSystem>().enabled = true;
            }
            else
            {
                this.GetComponent<StandaloneInputModule>().enabled = false;
                this.GetComponent<EventSystem>().enabled = false;
            }

        }
      
    }
}
