using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class TriggerPortal : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameController;
    public void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }



    IEnumerator WaitLoadController()
    {
        while (!GameObject.FindGameObjectWithTag("Player"))
            yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(1f);

        gameController = GameObject.FindGameObjectWithTag("GameController");

      //  print("on enable trigger portal");
      //  print(SceneManager.GetActiveScene().name);
    }
    private void OnEnable()
    {

       StartCoroutine(WaitLoadController());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            //  Debug.Log("hi");

            if (gameController != null)
                gameController.GetComponent<WinLose>().WinLevel();
            else
            {
                gameController = GameObject.FindGameObjectWithTag("GameController");
                gameController.GetComponent<WinLose>().WinLevel();

            }
        }
    }
}
