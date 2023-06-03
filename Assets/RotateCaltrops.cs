using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCaltrops : MonoBehaviour
{

    // Start is called before the first frame update
    private GameObject center;
    bool ok = false;
    bool fix = false;
    private void FixedUpdate()
    {/*
        if (!ok)
        {
            center = GameObject.FindGameObjectWithTag("Center");
            ok= center != null; 
        }
        if (ok && !fix)
        {
            //this.gameObject.GetComponent<Rigidbody2D>().freezeRotation= false;

            var rotation = center.GetComponent<Rigidbody2D>().rotation;

            this.gameObject.GetComponent<Rigidbody2D>().rotation = rotation + 90;
            //this.GetComponent<Rigidbody2D>().freezeRotation = true;
          //  Debug.Log("aaaaa");
            fix = true;
        }
        */
    }


}
