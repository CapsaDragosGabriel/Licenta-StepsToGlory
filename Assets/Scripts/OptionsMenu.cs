using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.IO;
public class OptionsMenu : MonoBehaviour
{
    public  AudioMixer audioMixer;

    public TMP_Dropdown resDropdown;
   public Resolution[] resolutions;
    public int currentResolutionIndex = 0;
    public int qualityIndex = 0;

    public SettingsGather settingsGather;
    public bool isFullscreen = true;
    public List<string> options = new List<string>();
    private void Start()
    {
        try
        {
            LoadSettingsJson();
        }
        catch {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {

                resolutions = Screen.resolutions;

                resDropdown.ClearOptions();



                for (int i = 0; i < resolutions.Length; i++)
                {
                    string option = resolutions[i].width + "x" + resolutions[i].height;
                    options.Add(option);

                    if (resolutions[i].width == Screen.currentResolution.width &&
                        resolutions[i].height == Screen.currentResolution.height)
                    {
                        currentResolutionIndex = i;
                    }
                }
                float temp;
                audioMixer.GetFloat("Volume", out temp);
                this.gameObject.GetComponentInChildren<Slider>().value = temp;

                resDropdown.AddOptions(options);
                resDropdown.value = currentResolutionIndex;
                resDropdown.RefreshShownValue();
            }
        }
  
    }


    public void SaveSettingsJson()
    {
        settingsGather = new SettingsGather();
        float temp;
        audioMixer.GetFloat("Volume", out temp);
        settingsGather.volume = temp;
        settingsGather.resolutionIndex= currentResolutionIndex;
        settingsGather.qualityIndex = qualityIndex;
        settingsGather.isFullscreen = isFullscreen;

        Debug.Log(settingsGather.isFullscreen);

        string json = JsonUtility.ToJson(settingsGather);
        File.WriteAllText(Application.persistentDataPath + "/Settings.json", json);
    }



    public void LoadSettingsJson()
    {
        resolutions = Screen.resolutions;

        string json = File.ReadAllText(Application.persistentDataPath + "/Settings.json");
        SettingsGather savedSettings = JsonUtility.FromJson<SettingsGather>(json);

        this.isFullscreen= savedSettings.isFullscreen;
        this.currentResolutionIndex = savedSettings.resolutionIndex;
        this.qualityIndex=savedSettings.qualityIndex;

        if (currentResolutionIndex > resolutions.Length) currentResolutionIndex = resolutions.Length - 1; 
        SetResolution(currentResolutionIndex);



        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
        }
        resDropdown.AddOptions(options);
        resDropdown.value = currentResolutionIndex;
        resDropdown.RefreshShownValue();
       
        SetFullscreen(isFullscreen);
        this.gameObject.GetComponentInChildren<Toggle>().isOn = isFullscreen;
        SetQuality(qualityIndex);
        this.gameObject.GetComponentsInChildren<TMP_Dropdown>()[0].value = qualityIndex;
        SetVolume(savedSettings.volume);
        gameObject.GetComponentInChildren<Slider>().value = savedSettings.volume;

    }
    public void SetVolume(float volume)
    {
       // volume=GetComponentInChildren<Slider>().value;
        audioMixer.SetFloat("Volume",volume);
    }

    public void SetQuality(int qualityIndex)
    {
        qualityIndex= 
        this.qualityIndex= qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        this.isFullscreen= isFullscreen;
        Screen.fullScreen=isFullscreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution= resolutions[resolutionIndex];
        currentResolutionIndex= resolutionIndex;   
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }
}


