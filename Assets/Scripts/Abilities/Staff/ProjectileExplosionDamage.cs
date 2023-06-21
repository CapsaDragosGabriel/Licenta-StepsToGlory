using UnityEngine;

public class ProjectileExplosionDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private float damage;
    float tickRate = 0.25f;
    public StatusEffectData _data;
    public GameObject projectilePrefab;
    public float bulletForce=30;
    public int baseNumberOfProjectiles = 7;

    public int numberOfProjectiles=7;
    public float radius = 1f;

    private void Start()
    {
        SpawnProjectiles();   
    }
   
    private void SpawnProjectiles()
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0;
        for (int i=0;i<numberOfProjectiles;i++)
        {
            float projectileDirXPos = this.gameObject.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPos = this.gameObject.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector= new Vector2(projectileDirXPos,projectileDirYPos);
            Vector2 projectileMoveDir=(projectileVector-new Vector2(this.transform.position.x,this.transform.position.y)).normalized*bulletForce;

            var proj = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity=new Vector2(projectileMoveDir.x,projectileMoveDir.y);
            
            float angleRotate = Mathf.Atan2(projectileMoveDir.y, projectileMoveDir.x) * Mathf.Rad2Deg - 90f;

            proj.GetComponent<Rigidbody2D>().rotation = angleRotate;
            angle += angleStep;

        }

    }
    public void SetDamage(float value)
    {
        damage = value;
    }
    public void SetTickRate(float tickrate)
    {
        this.tickRate = tickrate;
    }
}
