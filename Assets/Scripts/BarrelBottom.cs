using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BarrelBottom : MonoBehaviour
{
    // Start is called before the first frame update
    Color color;
    void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
        InvokeRepeating("fadeOut", 0.1f, 0.1f);
    }
    public void fadeOut()
    {

        var c = color;
        c.a -= 0.1f;
        GetComponent<SpriteRenderer>().color = c;
        color = c;

    }
  
}
