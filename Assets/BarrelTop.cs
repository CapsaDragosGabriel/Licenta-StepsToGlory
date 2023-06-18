using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BarrelTop : MonoBehaviour
{
    // Start is called before the first frame update
    int rotation;
    int changeDirection=1;
    Rigidbody2D rb;
    Color color;
    void Start()
    {
         rb = this.gameObject.GetComponent<Rigidbody2D>();
        Invoke("changeDir", 0.5f);
        color = GetComponent<SpriteRenderer>().color;
        while(rotation==0)
        rotation = Random.Range(-1, 2);
        InvokeRepeating("fadeOut", 0.1f, 0.1f);
    }
    public void changeDir()
    {
        changeDirection *= -1;
    }
    public void fadeOut()
    {
        
        var c = color;
        c.a -= 0.1f ;
      GetComponent<SpriteRenderer>().color =c;
        color = c;

    }
    private void FixedUpdate()
    {
        float x, y, spinZ;
        // if (rotation == 0) rotation = 1;
        
        x = Random.Range(1, 8)*rotation;
        y=Random.Range(10,30)*changeDirection;
        spinZ = Random.Range(1, 10) ;
        spinZ=rotation*spinZ;


        rb.AddForce(new Vector2(x, y)); 
        rb.AddTorque(spinZ);
       // this.transform.position = new Vector3(this.transform.position.x+ x, this.transform.position.y+y, 0);

       // this.transform.Rotate(0, 0, spinZ);
    
    }
}
