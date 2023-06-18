using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileExplosion : Ability
{
    float tickRate = 0.25f;
    [SerializeField]
    float baseDamage = 1.5f;
    public GameObject spriteInstance;
    float ap;
    float radius = 1f;
    // Start is called before the first frame update

    public override void Activate(GameObject parent)
    {

        ap = parent.GetComponent<StatsHolder>().getCurrStats().GetStatValue(StatType.ap);
        float totalDamage = baseDamage + ap * 0.4f;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        // sprite.GetComponent<MeteorDamage>().SetDamage(damage);

        spriteInstance = Instantiate(sprite);

        spriteInstance.GetComponent<ProjectileExplosionDamage>().SetDamage(totalDamage);
        spriteInstance.GetComponent<ProjectileExplosionDamage>().numberOfProjectiles = spriteInstance.GetComponent<ProjectileExplosionDamage>().baseNumberOfProjectiles + (int)ap / 5;


        activeTime = 2 * tickRate;

        spriteInstance.transform.localScale = new Vector3(radius, radius, 1);
        spriteInstance.transform.position = mousePos;
        Destroy(spriteInstance, activeTime);

    }

    public override void BeginCooldown(GameObject gameObject)
    {
        Destroy(spriteInstance);
    }

}
