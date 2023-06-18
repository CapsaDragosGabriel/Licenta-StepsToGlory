using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    // Update is called once per frame
    private bool canShoot = true;
    [SerializeField]
    private float baseAS = 0.8f;

    private float attackSpeed ;
    private float attackTimer = 0f;
    private Stats playerStats;
    public AudioSource gunSound;
    public void setShoot( bool shoot)
    {
       canShoot = shoot;
    }
    private void Start()
    {
        playerStats=GameObject.FindGameObjectWithTag("Player").GetComponent<StatsHolder>().getCurrStats();
    }
   
    void Update()
    {
        if(!canShoot)ShootingSpeed();

        if (Input.GetButtonDown("Fire1")&& canShoot)
        {
            Shoot();


            float vol;
            if (gunSound != null)
            {
                gunSound.outputAudioMixerGroup.audioMixer.GetFloat("Volume", out vol);
                AudioSource.PlayClipAtPoint(gunSound.clip, transform.position, Mathf.Min((vol + 60) / 1000, 1));
            }
            canShoot = false;

        }

    }


    void Shoot()
    {
        
       GameObject bullet=Instantiate(bulletPrefab, firePoint.position,firePoint.rotation);
        bullet.GetComponent<Bullet>().damage += Mathf.Pow(playerStats.GetStatValue(StatType.ad), 2) / Mathf.Pow(3,2);
        bullet.transform.Rotate(new Vector3(0, 0, 90));
       Rigidbody2D rb= bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        attackSpeed = Mathf.Max( baseAS - playerStats.GetStatValue((StatType)StatType.ad) / 10,0.1f);
    }


    private void  ShootingSpeed()
    {
        if (attackTimer >= attackSpeed) { canShoot = true; attackTimer = 0; }
        else attackTimer += Time.deltaTime;
    }
}
