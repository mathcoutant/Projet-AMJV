using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private GameObject mainMenuContainer;
    private GameObject settingsContainer;
    private GameObject characterContainer;

    // Start is called before the first frame update
    void Start()
    {
        settingsContainer = GameObject.Find("SettingsContainer");
        mainMenuContainer = GameObject.Find("MainMenuContainer");
        characterContainer = GameObject.Find("ChooseCharacterContainer");
        SwitchToMainMenuScreen();
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
}
