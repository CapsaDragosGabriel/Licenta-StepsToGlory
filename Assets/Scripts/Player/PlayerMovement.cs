using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    Vector2 movement;
    Vector2 mousePos;
    [SerializeField]
    private Camera cam;

    public GameObject center;
    //public GameObject firePoint;



    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 20f;
    private float dashingTime = 0.3f;
    private float dashingCooldown = 1f;

    public bool flipped = false;

    public TrailRenderer tr;
    public void setIsDashing(bool isDashing)
    {
        this.isDashing = isDashing;
    }
    private void Start()
    {

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        this.transform.position = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>().position;
    }
    void Update()
    {
        if (stopDash) isDashing = false;
        if (isDashing)
        {
            StartCoroutine(this.GetComponent<Invulnerability>().GetInvulnerable(dashingTime));
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (cam != null)
        { mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
           // Debug.Log("we cool");
        }
        else
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

        
    }

    private void FixedUpdate()
    {

        if (isDashing)
        { 
        
        return;
        }
        rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));

        gameObject.tag = "Player";
       
        Vector2 lookDir=mousePos- center.GetComponent<Rigidbody2D>().position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg-90f;


        if (lookDir.x < 0 && !flipped)
        {
            if (GameObject.FindGameObjectWithTag("Character"))
            {
                Vector3 currScale = GameObject.FindGameObjectWithTag("Character").transform.localScale;
                currScale.x *= -1;
                GameObject.FindGameObjectWithTag("Character").transform.localScale = currScale;
                if (GameObject.FindGameObjectWithTag("CharacterWeapon"))
                {
                    Vector3 currScaleWeap = GameObject.FindGameObjectWithTag("CharacterWeapon").transform.localScale;
                    if (currScaleWeap.y > 0)
                    {
                        currScaleWeap.y *= -1;

                    }
                    GameObject.FindGameObjectWithTag("CharacterWeapon").transform.localScale = currScaleWeap;
                }
                flipped = true;
            }
        }
        else if (flipped && lookDir.x >0)
        {
            if (GameObject.FindGameObjectWithTag("Character"))
            {
                Vector3 currScale = GameObject.FindGameObjectWithTag("Character").transform.localScale;
                currScale.x *= -1;
                GameObject.FindGameObjectWithTag("Character").transform.localScale = currScale;
                if (GameObject.FindGameObjectWithTag("CharacterWeapon"))
                {
                    Vector3 currScaleWeap = GameObject.FindGameObjectWithTag("CharacterWeapon").transform.localScale;
                   
                   if(currScaleWeap.y<0)
                        currScaleWeap.y *= -1;


                    GameObject.FindGameObjectWithTag("CharacterWeapon").transform.localScale = currScaleWeap;
                }    
                flipped = false;
            }
        }
        if (center != null)
        {

            center.GetComponent<Rigidbody2D>().rotation = angle;
            center.transform.position=this.transform.position;
        }



    }

    public void forcedDash(Vector3 direction)
    {
        if (!stopDash)
        {
        isDashing = true;
        stopDash = false;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower * movement.x, transform.localScale.y * dashingPower * movement.y);
        }
    }

    public void setDashing(bool isDashing) { this.isDashing = isDashing; }
    private IEnumerator Dash()  
    {
        canDash = false;
        isDashing= true;
      
        rb.velocity = new Vector2(transform.localScale.x * dashingPower * movement.x, transform.localScale.y * dashingPower * movement.y);
        tr.emitting = true;
        StartCoroutine(this.GetComponent<Invulnerability>().GetInvulnerable(dashingTime));

        yield return new WaitForSeconds(dashingTime);
            tr.emitting = false;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
       

    }

   public bool stopDash = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Loot")
        {
            stopDash = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Loot")
        {
            stopDash = false;
        }
    }
}
