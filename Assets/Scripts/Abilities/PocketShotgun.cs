using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class PocketShotgun : Ability
{
    float damage = 1f;
    public GameObject bullet;
    public float bulletForce = 20f;
    float spread = 3;
    public float dashVelocity=-30f;
    public override void Activate(GameObject parent)
    {


        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Rigidbody2D rb= parent.GetComponent<Rigidbody2D>();
        PlayerMovement movement=parent.GetComponent<PlayerMovement>();
        movement.setIsDashing(true);
        rb.velocity = (mousePos.normalized-rb.transform.position.normalized)* dashVelocity;


    }

    public override void BeginCooldown(GameObject gameObject)
    {
          PlayerMovement movement = gameObject.GetComponent<PlayerMovement>();

        movement.setIsDashing(false);
    }

}
