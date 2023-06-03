using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class WinLose : MonoBehaviour
{
    // Start is called before the first frame update
    private bool gameEnded;
    public GameObject player;
    public GameObject cam;
    public GameObject ui;
    public GameObject gameController;
    public GameObject eventSystem;

    //   private GameObject movePack;
    private Animator transition;
    private int nextIndex=1;
    public bool loadShop = true;
    int shopIndex = 2;
    public int getNextIndex() { return nextIndex; }
    private void Start()
    {
       
      //  movePack = GameObject.FindGameObjectWithTag("MovePack");

        transition = GetComponentInChildren<Animator>();
        //nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        GameObject ground = GameObject.FindGameObjectWithTag("Ground");
        if(ground!=null)
        ground.GetComponent<spawner2>().ForceSpawn();
    }

    private void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        eventSystem= GameObject.FindGameObjectWithTag("EventSystem");
        ui = GameObject.FindGameObjectWithTag("MainUI");
    }
    private void OnEnable()
    {
       

    }
    IEnumerator LoadSceneAsync()
    {

        transition.SetTrigger("Start");
        nextIndex = nextIndex + 1;
        if (nextIndex == shopIndex) nextIndex++;

        // Set the current Scene to be able to unload it later
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("ToDestroy"))
        {
            Destroy(go);

        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextIndex, LoadSceneMode.Additive);
        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(ui, SceneManager.GetSceneByBuildIndex(nextIndex));
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByBuildIndex(nextIndex));

        SceneManager.MoveGameObjectToScene(cam, SceneManager.GetSceneByBuildIndex(nextIndex));
        //SceneManager.MoveGameObjectToScene(eventSystem, SceneManager.GetSceneByBuildIndex(nextIndex));

        //  SceneManager.MoveGameObjectToScene(movePack, SceneManager.GetSceneByBuildIndex(nextIndex));


        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
        player.GetComponent<Transform>().position = GameObject.FindGameObjectsWithTag("Respawn")[1].transform.position;
        GameObject ground = GameObject.FindGameObjectWithTag("Ground");




        GameObject[] gameControllers = GameObject.FindGameObjectsWithTag("GameController");

        foreach (GameObject gc in gameControllers)
        {
            if (gc == this) continue;
            gc.GetComponent<WinLose>().loadShop = true;
            gc.GetComponent<WinLose>().nextIndex = this.nextIndex;

        }
        Destroy(gameController);
        ground.GetComponent<spawner2>().ForceSpawn();

        // print(GameObject.FindGameObjectsWithTag("Respawn")[nextIndex].transform.position);
    }

    IEnumerator LoadShopAsync()
    {
        transition.SetTrigger("Start");


        // Set the current Scene to be able to unload it later
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("ToDestroy"))
        {
            Destroy(go);

        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(shopIndex, LoadSceneMode.Additive);
        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(ui, SceneManager.GetSceneByBuildIndex(shopIndex));
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByBuildIndex(shopIndex));
        SceneManager.MoveGameObjectToScene(cam, SceneManager.GetSceneByBuildIndex(shopIndex));
        //SceneManager.MoveGameObjectToScene(eventSystem, SceneManager.GetSceneByBuildIndex(nextIndex));


        // SceneManager.MoveGameObjectToScene(movePack, SceneManager.GetSceneByBuildIndex(nextIndex));


        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
        player.GetComponent<Transform>().position = GameObject.FindGameObjectsWithTag("Respawn")[1].transform.position;

        GameObject[] gameControllers = GameObject.FindGameObjectsWithTag("GameController");

        foreach (GameObject gc in gameControllers)
        {
            if (gc == this) continue;
            gc.GetComponent<WinLose>().loadShop = false;
            gc.GetComponent<WinLose>().nextIndex = this.nextIndex;

        }
        Destroy(gameController);

       

        // print(GameObject.FindGameObjectsWithTag("Respawn")[nextIndex].transform.position);
    }
    public void WinLevel()
    {
        if (!gameEnded)
        {
            gameEnded= true;

            try
            {
                if (!loadShop) {
                    print("loading next scene");
                StartCoroutine("LoadSceneAsync");
                }
                else
                {
                    print("loading shop");

                    StartCoroutine(LoadShopAsync());
                }

            }
            catch (Exception e)
            {
                Debug.LogError("Scene not valid");
                Debug.LogError(e);
            }
        
        }
    }
    public void LoseLevel()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
