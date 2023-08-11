using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class StartScript : MonoBehaviour
{
    private GameObject MainMenu;
    private GameObject StartMenu;
    private GameObject SettingsMenu;

    private AudioMixer audioMixer;
    private Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    void Start()
    {
        GameObject menus = transform.Find("Menus").gameObject;

        MainMenu = menus.transform.Find("MainMenu").gameObject;
        StartMenu = menus.transform.Find("StartMenu").gameObject;
        SettingsMenu = menus.transform.Find("SettingsMenu").gameObject;

        MainMenu.SetActive(true);
        StartMenu.SetActive(false);
        SettingsMenu.SetActive(false);

        audioMixer = (AudioMixer)Resources.Load("audio/MainMixer", typeof(AudioMixer));

        resolutionDropdown = SettingsMenu.transform.Find("Resolution").GetComponent<Dropdown>();

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResoltionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResoltionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResoltionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
