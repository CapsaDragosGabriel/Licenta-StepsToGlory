using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowFlip : MonoBehaviour
{

    public GameObject archer;
    // Start is called before the first frame update
    public bool currFlip;
    public int nr = -1;
    private void Start()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (currFlip==archer.GetComponent<SpriteRenderer>().flipX)
        {
           
        }
        else
        {

            currFlip = !currFlip;
            GetComponent<SpriteRenderer>().flipX = currFlip;
            transform.localPosition = new Vector3(transform.localPosition.x * nr, transform.localPosition.y, transform.localPosition.z) ;
        }
    }
}
