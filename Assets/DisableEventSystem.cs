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
        if (Input.GetKeyUp(KeyCode.C))
        {

            this.GetComponent<StandaloneInputModule>().enabled = !this.GetComponent<StandaloneInputModule>().enabled;
            this.GetComponent<EventSystem>().enabled = !this.GetComponent<EventSystem>().enabled;

        }
    }
}
