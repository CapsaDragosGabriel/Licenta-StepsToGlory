using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class MainMenu : MonoBehaviour
{
    public GameObject gunner;
    public GameObject warrior;
    public GameObject sorcerer;
    public GameObject selected;
    public List<Image> images= new List<Image>();

    public GameObject camera;
    public GameObject UI;

    public TMPro.TMP_InputField monitoredField;
    public GameObject mainOptions;
    public string playerName="Anonymous";
    public void Start()
    {
        images[0].sprite=gunner.GetComponent<SpriteRenderer>().sprite;
        images[0].color = gunner.GetComponent<SpriteRenderer>().color;

        images[1].sprite = warrior.GetComponent<SpriteRenderer>().sprite;
        images[1].color = warrior.GetComponent<SpriteRenderer>().color;

        images[2].sprite = sorcerer.GetComponent<SpriteRenderer>().sprite;
        images[2].color = sorcerer.GetComponent<SpriteRenderer>().color;
        selected = gunner;
      //  mainOptions = GameObject.FindGameObjectWithTag("OptionsMenu");
        //GameObject.FindGameObjectWithTag("OptionsMenu").SetActive(false);
        //this.gameObject.SetActive(false);
    }
    public void SelectWarrior()
    {
        selected = warrior;
    }

    public void ChangeName()
    {
        playerName = monitoredField.text;
    }
    public void SelectGunner ()
    {
        selected = gunner;
    }
    public void SelectSorcerer()
    {
        selected = sorcerer;
    }

    IEnumerator LoadSceneAsync()
    {
        //transition.SetTrigger("Start");

        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene

        GameObject instance=Instantiate(selected);
        if (playerName!="")
        instance.GetComponent<StatsHolder>().setPlayerName (playerName);
        else instance.GetComponent<StatsHolder>().setPlayerName("Anonymous");
        SceneManager.MoveGameObjectToScene(instance, SceneManager.GetSceneByBuildIndex(1));
        GameObject instanceCam = Instantiate(camera);
        SceneManager.MoveGameObjectToScene(instanceCam, SceneManager.GetSceneByBuildIndex(1));
        GameObject instanceUI = Instantiate(UI);
        instanceUI.GetComponentInChildren<OptionsMenu>().audioMixer = mainOptions.GetComponent<OptionsMenu>().audioMixer;
        float x;
         mainOptions.GetComponent<OptionsMenu>().audioMixer.GetFloat("Volume",out x) ;
        instanceUI.GetComponentInChildren<OptionsMenu>().SetVolume(x);
        instanceUI.GetComponentInChildren<Slider>().value=x;
        instanceUI.GetComponentsInChildren<TMP_Dropdown>()[0].value = mainOptions.GetComponentsInChildren<TMP_Dropdown>()[0].value;
        instanceUI.GetComponentInChildren<Toggle>().isOn = mainOptions.GetComponentInChildren<Toggle>().isOn;
        instanceUI.GetComponentsInChildren<TMP_Dropdown>()[1].value = mainOptions.GetComponentsInChildren<TMP_Dropdown>()[1].value;


        // instanceUI.GetComponentInChildren<OptionsMenu>().resDropdown = mainOptions.GetComponentInChildren<OptionsMenu>().resDropdown;

        //  Debug.Log(mainOptions.GetComponentInChildren<OptionsMenu>().resDropdown.options);

        instanceUI.GetComponentInChildren<OptionsMenu>().resolutions = mainOptions.GetComponentInChildren<OptionsMenu>().resolutions;
        instanceUI.GetComponentInChildren<OptionsMenu>().currentResolutionIndex = mainOptions.GetComponentInChildren<OptionsMenu>().currentResolutionIndex;
        Debug.Log(instanceUI.GetComponentInChildren<OptionsMenu>().resolutions);
        instanceUI.GetComponentInChildren<OptionsMenu>().resDropdown.ClearOptions();
        instanceUI.GetComponentInChildren<OptionsMenu>().resDropdown.AddOptions(mainOptions.GetComponentInChildren<OptionsMenu>().options);
        instanceUI.GetComponentInChildren<OptionsMenu>().resDropdown.value = mainOptions.GetComponentInChildren<OptionsMenu>().currentResolutionIndex;
        instanceUI.GetComponentInChildren<OptionsMenu>().resDropdown.RefreshShownValue();

        // StartCoroutine(UpdateResolution(instanceUI));

        // instanceUI.GetComponentInChildren<OptionsMenu>().resDropdown =mainOptions.resDropdown;

        SceneManager.MoveGameObjectToScene(instanceUI, SceneManager.GetSceneByBuildIndex(1));

        /* GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
         foreach (GameObject player in players)
         {
             Debug.Log(player);
             if (player.GetComponent<StatsHolder>().getClass() == selected.GetComponent<StatsHolder>().getClass())
             {
                 player.SetActive(true);
             }
         }*/

        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
        


        GameObject ground = GameObject.FindGameObjectWithTag("Ground");




        GameObject[] gameControllers = GameObject.FindGameObjectsWithTag("GameController");

        ground.GetComponent<spawner2>().ForceSpawn();

        // print(GameObject.FindGameObjectsWithTag("Respawn")[nextIndex].transform.position);
    }

    public IEnumerator UpdateResolution(GameObject instanceUI)
    {
        yield return new WaitForSeconds(1f);
       
    }
    public void PlayGame()
    {
        Scene scene=SceneManager.GetActiveScene();
        Debug.Log(selected);
        StartCoroutine(LoadSceneAsync());

        /*  GameObject instance = Instantiate(selected);
          instance.GetComponent<PlayerHealth>().enabled = false;
          instance.GetComponent<PlayerMovement>().enabled = false;
          instance.GetComponent<StatsHolder>().enabled = false;

          SceneManager.LoadScene("Level_1");
          SceneManager.MoveGameObjectToScene(instance, SceneManager.GetSceneByBuildIndex(1));
          instance.GetComponent<PlayerHealth>().enabled = true;
          instance.GetComponent<PlayerMovement>().enabled = true;
          instance.GetComponent<StatsHolder>().enabled = true;*/
        //        SceneManager.UnloadScene(scene);
        //      SceneManager.MoveGameObjectToScene(selected, nextScene);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
