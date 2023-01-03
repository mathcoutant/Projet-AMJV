using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{

    private GameObject mainMenuContainer;
    private GameObject settingsContainer;
    private GameObject characterContainer;
    public Slider volumeSlider;
    public Slider effectSlider;
    public Slider musicSlider;

    // Start is called before the first frame update
    void Start()
    {
        // Setting the different screens in the menu
        settingsContainer = GameObject.Find("SettingsContainer");
        mainMenuContainer = GameObject.Find("MainMenuContainer");
        characterContainer = GameObject.Find("ChooseCharacterContainer");
        SwitchToMainMenuScreen();

        // Setting the different sliders for the settings screen
        volumeSlider.value = 1;
        GameObject volumeText = volumeSlider.transform.parent.gameObject.transform.Find("Percentage").gameObject;
        volumeSlider.onValueChanged.AddListener(delegate { SliderValueChange(volumeSlider, volumeText); });

        effectSlider.value = 1;
        GameObject effectText = effectSlider.transform.parent.gameObject.transform.Find("Percentage").gameObject;
        effectSlider.onValueChanged.AddListener(delegate { SliderValueChange(effectSlider, effectText); });

        musicSlider.value = 1;
        GameObject musicText = musicSlider.transform.parent.gameObject.transform.Find("Percentage").gameObject;
        musicSlider.onValueChanged.AddListener(delegate { SliderValueChange(musicSlider, musicText); });

    }

    public void SwitchToMainMenuScreen()
    {
        mainMenuContainer.SetActive(true);
        settingsContainer.SetActive(false);
        characterContainer.SetActive(false);
    }

    public void SwitchToSettingsScreen()
    {
        mainMenuContainer.SetActive(false);
        settingsContainer.SetActive(true);
        characterContainer.SetActive(false);
    }
    public void SwitchToCharacterScreen()
    {
        mainMenuContainer.SetActive(false);
        settingsContainer.SetActive(false);
        characterContainer.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    void SliderValueChange(Slider slider, GameObject textObject)
    {
        // Change the text in the indicator in percentage
        textObject.GetComponent<TextMeshProUGUI>().text = (slider.value * 100).ToString("F0") +"%";
    }



}
