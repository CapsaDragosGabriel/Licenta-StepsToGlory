using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoInstance : MonoBehaviour
{
    // Start is called before the first frame update
    public static MonoInstance instance;

    private void Start()
    {
        MonoInstance.instance = this;
    }
}
