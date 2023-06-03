using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;
using System.Buffers.Text;

public class HealerAI : MonoBehaviour, IEffecteable
{
    private StatusEffectData _data;

    private Transform target;
    public float speed = 400f;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndPath = false;
    public bool isStunned = false;
    private Collider2D aggroRange;

    [SerializeField]
    private GameObject enemyHealPrefab;

    public bool canUseHeal = true;
    public bool isUsingHeal = false;
    private float healCooldown = 3f;
    [SerializeField]
    private float healDamage = 2f;
    [SerializeField]
    private float healValue = 2f;
    private float healActiveTime = 3f;
    private float healTickRate = 1.5f;


    private bool canShoot = true;
    private bool targetLocked = false;

    bool reachedRange = false;

    private SpriteRenderer spriteRenderer;

    Seeker seeker;
    Rigidbody2D rb;


   
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Your Function You Want to Call
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart(0.5f));

        aggroRange = GetComponent<CircleCollider2D>();
        seeker = GetComponent<Seeker>();
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

       // Debug.Log(target);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        Physics.IgnoreLayerCollision(1, 1);

    }

    void UpdatePath()
    {
        if (target != null)
            if (aggroRange.IsTouching(target.GetComponent<CircleCollider2D>())
                || targetLocked
                || this.GetComponent<EnemyHealth>().maxHealth != this.GetComponent<EnemyHealth>().health)
            {
                targetLocked = true;
                if (seeker.IsDone())
                {
                    if (target != null)
                    {

                        // Vector2 rangedPos=new Vector2((rb.position.x+target.position.x)/2,(rb.position.y+target.position.y)/2);
                        float distance = Vector2.Distance(rb.position, target.position);
                        if (distance > 10)
                        {
                            seeker.StartPath(rb.position, target.position, OnPathComplete);
                            reachedRange = false;
                        }
                        else
                        {
                            reachedRange = true;
                            rb.velocity = Vector3.zero;
                         //   Debug.Log("stop");
                        }
                    }
                }
            }
            else
            {
                Vector2 newDirection = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
                    seeker.StartPath(rb.position, rb.position + newDirection, OnPathComplete);
                
            }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;


        }
    }
    // Update is called once per frame


    void FixedUpdate()
    {
        if (!canShoot) ShootingSpeed();

        if (_data != null) HandleEffect();
        if (isStunned) return;

        float range = 0;
        if (target != null)
            range = Vector2.Distance(rb.position, target.position);
        if (range < 10)
        {
            reachedRange = true;
        }



if (targetLocked && reachedRange)
        {
            
            if (target != null)
            {


                bool needToHeal = SearchDamagedEnemy();
                //Debug.Log(needToHeal);

                if (canUseHeal&& needToHeal)
                {
           
                        //Debug.Log(damagedEnemyPosition);

                        CastHeal(damagedEnemyPosition);
                        //Debug.Log("need to heal: " + needToHeal);

                }
                else if (canShoot)
                {
                    Shoot();
                    canShoot= false; 
                }
               

            }

        }
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndPath = true;
            return;

        }
        else
        {
            reachedEndPath = false;
        }


        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        Vector2 force = direction * speed * Time.deltaTime;
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (!reachedRange)
            rb.AddForce(force);



        if (rb.velocity.x >= 0.01f && force.x > 0f)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.velocity.x <= -0.01 && force.x < 0f)
        {
            spriteRenderer.flipX = true;


        }

    }

    private float attackTimer = 0f;
    private float attackSpeed = 1f;
    public GameObject bulletPrefab;
    public float damage = 1f;
    public float bulletForce = 2f;
    Vector3 damagedEnemyPosition = Vector3.zero;
    private List<Collider2D> colliders=new List<Collider2D>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!colliders.Contains(collision)) colliders.Add(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!colliders.Contains(collision)) colliders.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    }
    private bool SearchDamagedEnemy()
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider)
                if (collider.tag == "Enemy")
                        if (!collider.GetComponent<EnemyHealth>().isFull())
                        {
                           // Debug.Log("Heals please");
                            damagedEnemyPosition = collider.GetComponent<Transform>().position;
                            return true;
                        }
        }
        return false;
    }
    void CastHeal(Vector3 position)
    {
        
        {
            GameObject healInstance = Instantiate(enemyHealPrefab);
            healInstance.GetComponent<Transform>().position = position;
            healInstance.GetComponent<enemyHeal>().SetDamage(healDamage);
            healInstance.GetComponent<enemyHeal>().SetHeal(healValue);
            healInstance.GetComponent<enemyHeal>().SetTickRate(healTickRate);
            healInstance.GetComponent<enemyHeal>().StartDamage();

            isUsingHeal = true;
           // canShoot = false;
            canUseHeal = false;
            StartCoroutine("ResetHeal");
            damagedEnemyPosition=Vector3.zero; 
            Destroy(healInstance, healActiveTime);
        }
    }

    public IEnumerator ResetHeal()
    {
        yield return new WaitForSeconds(healActiveTime);
        isUsingHeal = false;
        //canShoot = true;
        yield return new WaitForSeconds(healCooldown);
        canUseHeal = true;
    }
    
    void Shoot()
    {

        Transform firePoint = this.GetComponent<Transform>();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        bullet.GetComponent<EnemyBullet>().damage = damage;

        Rigidbody2D rbbullet = bullet.GetComponent<Rigidbody2D>();
        if (target != null)
        {
            //aruncat sageata spre jucator
            Vector2 shootDirection = (Vector2)target.position - (Vector2)firePoint.position;
            shootDirection = Quaternion.Euler(shootDirection.normalized) * shootDirection.normalized;
            rbbullet.AddForce(shootDirection, ForceMode2D.Impulse);
            rbbullet.velocity = rbbullet.velocity.normalized * bulletForce;
            //setat rotatia sagetii
            Vector2 lookDir = (Vector2)target.position - (Vector2)rbbullet.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            rbbullet.rotation = angle;
            rbbullet.freezeRotation = true;
        
            Destroy(bullet, 4);
           
        }


    }

    private void ShootingSpeed()
    {
        if (attackTimer >= attackSpeed) { canShoot = true; attackTimer = 0; }
        else attackTimer += Time.deltaTime;
    }

    public IEnumerator ResetShooting()
    {
        yield return new WaitForSeconds(attackSpeed);
        canShoot = true;

    }
    

    private float _currentEffectTimer = 0f;
    private float _nextTickTime = 0f;
    public void HandleEffect()
    {
        if (_data == null) return;
        _currentEffectTimer += Time.deltaTime;

        if (_data.DOTAmount != 0 && _currentEffectTimer > _nextTickTime)
        {
            _nextTickTime += _data.tickSpeed;
            this.GetComponent<EnemyHealth>().TakeDamage((int)_data.DOTAmount);
        }

        if (_currentEffectTimer > _data.duration)
        {
            RemoveEffect();
        }

    }
    private GameObject _effectParticles;
    public void ApplyEffect(StatusEffectData _data)
    {
        this._data = _data;
        if (_data != null)
            if (_data.EffectParticles != null)
                _effectParticles = Instantiate(_data.EffectParticles, transform);

    }
    public void RemoveEffect()
    {
        _data = null;
        _currentEffectTimer = 0f;
        _nextTickTime = 0f;
        if (_effectParticles != null) Destroy(_effectParticles);
    }
}
