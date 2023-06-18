using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameScript :MonoBehaviour
{   // Start is called before the first frame update
    public GameObject options;
    public void Start()
    {
      options.SetActive(true);
        options.transform.localScale = Vector3.zero;
        StartCoroutine("deactivate");

    }
    public IEnumerator deactivate()
    {
        yield return new WaitForSeconds(0.3f);
        options.transform.localScale = Vector3.one;

        options.SetActive(false);
    }
    public void ExitGame()
{
        Application.Quit();
}
}
