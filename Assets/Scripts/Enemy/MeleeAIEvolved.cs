using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class MeleeAIEvolved : MonoBehaviour, IEffecteable
{
    private StatusEffectData _data;

    private Transform target;
    public float speed = 400f;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndPath = false;

    private Collider2D aggroRange;

    public bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 30f;
    private float dashingTime = 0.3f;
    private float dashingCooldown = 5f;
    private bool targetLocked = false;

    private CircleCollider2D slamAggroCollider;
    private float enableSpellsTimer = 1f;

    private SpriteRenderer spriteRenderer;

    Seeker seeker;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(1, 1);
        aggroRange = GetComponents<CircleCollider2D>()[0];
        seeker = GetComponent<Seeker>();
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        //Debug.Log(target);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        slamAggroCollider = this.GetComponents<CircleCollider2D>()[1];

    }

    void UpdatePath()
    {

        if (target != null)
            if (aggroRange.IsTouching(target.GetComponent<CircleCollider2D>())
                    || targetLocked
                    || this.GetComponent<EnemyHealth>().maxHealth != this.GetComponent<EnemyHealth>().health)
            {
                //if (enableSpellsTimer > 0f) enableSpellsTimer -= Time.fixedDeltaTime;
              //  if (!castSpells) StartCoroutine("enableSpells");
                targetLocked = true;
                if (seeker.IsDone())
                {
                    if (target != null)
                        seeker.StartPath(rb.position, target.position, OnPathComplete);

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

    public bool isStunned = false;
    void FixedUpdate()
    {
        // if (!invoked) { InvokeRepeating("UpdatePath", 0f, 0.5f); invoked = true; }
        if (_data != null) HandleEffect();
        if (isStunned) return;
        if (path == null)
            return;
        if (isDashing)
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
        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }


    
        if (direction != null && canDash && targetLocked )
        {
            StartCoroutine(DashSlam(direction));
            // StartCoroutine("Slam");
        }
        

        rb.AddForce(force);



        if (rb.velocity.x >= 0.01f && force.x > 0f)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.velocity.x <= -0.01 && force.x < 0f)
        {
            spriteRenderer.flipX = true;


        }
        if (target)
            if (Vector2.Distance(this.transform.position, target.transform.position) < slamAggroCollider.radius)
                if (slamAggroCollider.IsTouching(target.GetComponent<CircleCollider2D>()))
        {
                    StartCoroutine("Slam");
        }
       
    }
    [SerializeField]
    private GameObject slamEffect;
    [SerializeField]
    private float slamDamage = 3f;
    [SerializeField]
    private float slamCooldown = 2f;
    [SerializeField]
    private float slammingTime = 2.5f;
    [SerializeField]
    private bool canSlam = true;
    private bool isSlamming = false;
    private IEnumerator Slam()
    {
        if (canSlam)
        {
            GameObject slamInstance = Instantiate(slamEffect);
            slamInstance.transform.position = this.transform.position;
            slamInstance.GetComponent<EnemySlam>().SetDamage(slamDamage);
            slamInstance.GetComponent<EnemySlam>().SetTickRate(slammingTime / 3);
            slamInstance.GetComponent<EnemySlam>().StartDamage();
            Destroy(slamInstance,slammingTime);

            var currVelocity = rb.velocity;
           // rb.velocity = Vector2.zero;
            isSlamming = true;
            canSlam = false;
            yield return new WaitForSeconds(slammingTime);
           // rb.velocity = currVelocity;
            isSlamming = false;
            yield return new WaitForSeconds(slamCooldown);
            canSlam = true;
        }
      
    }

    private IEnumerator DashSlam(Vector2 movement)
    {
        canDash = false;
        isDashing = true;

        rb.velocity = new Vector2(transform.localScale.x * dashingPower*5* movement.x, transform.localScale.y * dashingPower*5 * movement.y);
        yield return new WaitForSeconds(dashingTime);
      StartCoroutine("Slam");

        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;


    }


   
    private float _currentEffectTimer = 0f;
    private float _nextTickTime = 0f;
  

    bool slowApplied = false;

    public void HandleEffect()
    {

        if (_data == null) return;
        _currentEffectTimer += Time.deltaTime;
        if (_data.DOTAmount != 0 && _currentEffectTimer > _nextTickTime)
        {
            _nextTickTime += _data.tickSpeed;
            this.GetComponent<EnemyHealth>().TakeDamage((int)_data.DOTAmount);




            if (_data.movementPenalty != 0 && slowApplied == false)
            {
                slowApplied = true;
                this.speed = this.speed - _data.movementPenalty;

            }
        }

        if (_currentEffectTimer > _data.duration)
        {
            if (slowApplied) this.speed += _data.movementPenalty;
            slowApplied = false;
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
