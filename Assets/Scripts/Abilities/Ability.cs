 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    new public string name;
    public float baseCooldown;

    public float cooldownTime;
    public float activeTime;
    public GameObject sprite;
    public void Awake()
    {
        cooldownTime = baseCooldown;
    }
    public float getCurrentCooldown()
    {
        return 0;
    }

    
    public virtual void Activate(GameObject gameObject) { }
    public virtual void BeginCooldown(GameObject gameObject) { }


}
