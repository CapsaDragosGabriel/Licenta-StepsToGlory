using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class EnemyAIMelee : MonoBehaviour,IEffecteable
{
    private StatusEffectData _data;

    private Transform target;
    public float speed = 400f;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndPath = false;

    private Collider2D aggroRange;


    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 20f;
    private float dashingTime = 0.3f;
    private float dashingCooldown = 3f;
    private bool targetLocked = false;



    private SpriteRenderer spriteRenderer;

    Seeker seeker;
    Rigidbody2D rb;

   
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(1, 1);
        aggroRange=GetComponent<CircleCollider2D>();
        seeker=GetComponent<Seeker>();
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

           // Debug.Log(target);
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }

    void UpdatePath()
    {
        
        if (target!=null)
        if (aggroRange.IsTouching(target.GetComponent<CircleCollider2D>())
                || targetLocked 
                || this.GetComponent<EnemyHealth>().maxHealth != this.GetComponent<EnemyHealth>().health)
        {
            targetLocked= true;
            if (seeker.IsDone())
            {
                if (target != null)
                    seeker.StartPath(rb.position, target.position, OnPathComplete);

            }
        }
        else
        { 
            Vector2 newDirection=new Vector2(Random.Range(-5,5), Random.Range(-5,5));
                try
                {
                    seeker.StartPath(rb.position, rb.position + newDirection, OnPathComplete);

                }
                catch (System.Exception)
                {

                    
                }
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

        int dice = Random.Range(0, 500);
        if (dice <= 2 && canDash && targetLocked)
        {

          
            if (direction!=null)
            {
                StartCoroutine(Dash(direction));
            }
        }

        rb.AddForce(force);
        


        if (rb.velocity.x >= 0.01f && force.x > 0f)
        {
            spriteRenderer.flipX= false;
        }
        else if (rb.velocity.x <= -0.01 && force.x < 0f)
        {
            spriteRenderer.flipX = true;


        }

    }
    private IEnumerator Dash(Vector2 movement)
    {
        canDash = false;
        isDashing = true;

        rb.velocity = new Vector2(transform.localScale.x * dashingPower * movement.x, transform.localScale.y * dashingPower * movement.y);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;


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

        if (_currentEffectTimer> _data.duration)
        {
            RemoveEffect();
        }
        
    }
    private GameObject _effectParticles;
    public void ApplyEffect(StatusEffectData _data)
    {
        this._data = _data;
        if (_data!=null)
        if (_data.EffectParticles != null)
            _effectParticles= Instantiate(_data.EffectParticles, transform);

    }
    public void RemoveEffect()
    {
        _data = null;
        _currentEffectTimer = 0f;
        _nextTickTime = 0f;
        if (_effectParticles!=null) Destroy(_effectParticles); 
    }
}
