using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentialAbilities : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<Ability> gunnerAbilities;
    [SerializeField]
    List<Ability> sorcererAbilities;
    [SerializeField]
    List<Ability> warriorAbilities;

    public List<Ability> getAbilities(Classes cls)
    {
        switch(cls)
        {
            case Classes.Warrior:return warriorAbilities;
            case Classes.Sorcerer: return sorcererAbilities;
            case Classes.Gunner: return gunnerAbilities;
            default:    return null;
        }

    }
}
