using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="StatusEffect")]
public class StatusEffectData : ScriptableObject
{
    new public string name;
    public float DOTAmount;
    public float duration;
    public float movementPenalty;
    public float tickSpeed;

    public GameObject EffectParticles;
}
