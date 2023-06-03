using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCharacterStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.C)) {
         
            this.GetComponent<Canvas>().enabled = !this.GetComponent<Canvas>().enabled;
        }
    }
}
