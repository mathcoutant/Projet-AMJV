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
    public TMP_Dropdown resolutionDropdown;
    private GameObject nameDescriptionText;
    private GameObject descriptionText;
    private GameObject victoryIndicator;
    private Image characterImage;
    [SerializeField] private Sprite warriorImage = null;
    [SerializeField] private Sprite mageImage = null;
    [SerializeField] private Sprite archerImage = null;

    // Start is called before the first frame update
    void Start()
    {
        characterImage = GameObject.Find("CharacterImageContainer").GetComponent<Image>();
        warriorImage = Resources.Load<Sprite>("Images/CharacterImages/canardtrosad");
        mageImage = Resources.Load<Sprite>("Images/CharacterImages/logoDiscord");
        archerImage = Resources.Load<Sprite>("Images/CharacterImages/logoDiscord");

        // Setting the text object for the characters descriptions
        nameDescriptionText = GameObject.Find("CharacterNameText");
        descriptionText = GameObject.Find("CharacterDescriptionText");
        victoryIndicator = GameObject.Find("VictoryIndicator");
        ChangeDescription(0);

        // Setting the different screens in the menu
        settingsContainer = GameObject.Find("SettingsContainer");
        mainMenuContainer = GameObject.Find("MainMenuContainer");
        characterContainer = GameObject.Find("ChooseCharacterContainer");
        SwitchToMainMenuScreen();

        // Setting the different sliders for the settings screen
        volumeSlider.value = 1;
        TextMeshProUGUI volumeText = volumeSlider.transform.parent.gameObject.transform.Find("Percentage").gameObject.GetComponent<TextMeshProUGUI>();
        volumeSlider.onValueChanged.AddListener(delegate { SliderValueChange(volumeSlider, volumeText); });

        effectSlider.value = 1;
        TextMeshProUGUI effectText = effectSlider.transform.parent.gameObject.transform.Find("Percentage").gameObject.GetComponent<TextMeshProUGUI>();
        effectSlider.onValueChanged.AddListener(delegate { SliderValueChange(effectSlider, effectText); });

        musicSlider.value = 1;
        TextMeshProUGUI musicText = musicSlider.transform.parent.gameObject.transform.Find("Percentage").gameObject.GetComponent<TextMeshProUGUI>();
        musicSlider.onValueChanged.AddListener(delegate { SliderValueChange(musicSlider, musicText); });

        // Add listener to the dropdown
        resolutionDropdown.onValueChanged.AddListener(delegate {
            ResolutionDropdownValueChanged();
        });
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

    private void SliderValueChange(Slider slider, TextMeshProUGUI textObject)
    {
        // Change the text in the indicator in percentage
        textObject.text = (slider.value * 100).ToString("F0") +"%";
    }

    private void ResolutionDropdownValueChanged()
    {
        switch(resolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, false);
                break;
            case 2:
                Screen.SetResolution(1280, 720, false);
                break;
            case 3:
                Screen.SetResolution(720, 480, false);
                break;
            default:
                Screen.SetResolution(1920, 1080, false);
                break;
        }
    }

   public void ChangeDescription(int i)
    {
        string desc = "";
        string[] tab = new string[] {Hero.nameClass, Hero.maxHealth.ToString(), Hero.waveReached.ToString(), Hero.timesPlayed.ToString(), Hero.hasWon.ToString() };
        if (i == 0) { tab = new string[] { Warrior.nameClass, Warrior.maxHealth.ToString(), Warrior.waveReached.ToString(), Warrior.timesPlayed.ToString(), Warrior.hasWon.ToString() }; };
        if (i == 1) { tab = new string[] { Mage.nameClass, Mage.maxHealth.ToString(), Mage.waveReached.ToString(), Mage.timesPlayed.ToString(), Mage.hasWon.ToString() }; };
        //if (i == 2) { tab = new string[] { Archer.nameClass, Archer.maxHealth.ToString(), Archer.waveReached.ToString(), Archer.timesPlayed.ToString(), Archer.hasWon.ToString() }; };

        desc += "Health : " + tab[1] + "\n" + "\n";
        //desc += "Difficulty : " + "" + "\n";
        desc += "Max wave reached : " + tab[2] + "\n";
        desc += tab[3] + " games played";

        nameDescriptionText.GetComponent<TextMeshProUGUI>().text = tab[0];
        descriptionText.GetComponent<TextMeshProUGUI>().text = desc;
        if (i == 0) { characterImage.sprite = warriorImage; }
        if (i == 1) { characterImage.sprite = mageImage; }
        if (i == 2) { characterImage.sprite = archerImage; }
        if (tab[4] == "True")
        {
            victoryIndicator.SetActive(true);
        } else
        {
            victoryIndicator.SetActive(false);
        }
    }

}
