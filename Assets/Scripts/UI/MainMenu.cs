using System;
using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{

    public Slider volumeSlider;
    public Slider effectSlider;
    public Slider musicSlider;
    public TMP_Dropdown resolutionDropdown;
    private GameObject characterContainer;
    private Image characterImage;
    private GameObject descriptionText;

    private GameObject mainMenuContainer;
    private GameObject nameDescriptionText;
    private GameObject settingsContainer;
    private GameObject victoryIndicator;
    private string selectedString;
    [SerializeField] private Sprite warriorImage = null;
    [SerializeField] private Sprite mageImage = null;
    [SerializeField] private Sprite rogueImage = null;
    AudioManager audioManager;
    AudioSource settingsEffectSampleSound;
    private bool canPlayEffectSample = true;

    // Start is called before the first frame update
    private void Start()
    {
        characterImage = GameObject.Find("CharacterImageContainer").GetComponent<Image>();
        warriorImage = Resources.Load<Sprite>("Images/CharacterImages/warrior");
        mageImage = Resources.Load<Sprite>("Images/CharacterImages/mage");
        rogueImage = Resources.Load<Sprite>("Images/CharacterImages/rogue");

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
        TextMeshProUGUI volumeText = volumeSlider.transform.parent.gameObject.transform.Find("Percentage").gameObject.GetComponent<TextMeshProUGUI>();
        volumeSlider.onValueChanged.AddListener(delegate { SliderValueChange(volumeSlider, volumeText); });

        TextMeshProUGUI effectText = effectSlider.transform.parent.gameObject.transform.Find("Percentage").gameObject.GetComponent<TextMeshProUGUI>();
        effectSlider.onValueChanged.AddListener(delegate { SliderValueChange(effectSlider, effectText); });

        TextMeshProUGUI musicText = musicSlider.transform.parent.gameObject.transform.Find("Percentage").gameObject.GetComponent<TextMeshProUGUI>();
        musicSlider.onValueChanged.AddListener(delegate { SliderValueChange(musicSlider, musicText); });

        // Add listener to the dropdown
        resolutionDropdown.onValueChanged.AddListener(delegate {
            ResolutionDropdownValueChanged();
        });

        // Setting the objects for the audio
        audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();
        settingsEffectSampleSound = GameObject.Find("EffectSample").GetComponent<AudioSource>();
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

        CharacterSelection.classSelection = selectedString;
        audioManager.SaveVolumeValues();
        SceneManager.LoadScene(1);
        SceneManager.LoadScene(2,LoadSceneMode.Additive);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);

    }

    public void QuitGame()
    {
        audioManager.SaveVolumeValues();
        Application.Quit();
    }

    private void SliderValueChange(Slider slider, TextMeshProUGUI textObject)
    {
        // Change the text in the indicator in percentage
        textObject.text = (slider.value * 100).ToString("F0") + "%";
    }
    public void PlayEffectSampleSound()
    {
        if(canPlayEffectSample)
        {
            settingsEffectSampleSound.Play();
            canPlayEffectSample = false;
            StartCoroutine(DelayEffectSample());
        }
    }
    IEnumerator DelayEffectSample()
    {
        yield return new WaitForSeconds(0.1f);
        canPlayEffectSample = true;
    }
    private void ResolutionDropdownValueChanged()
    {
        switch (resolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, true);
                Screen.fullScreen = true;
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
        if (i == 0) { 
            Warrior selectedHero = new Warrior();
            selectedString = "Warrior";
            desc += "Health : " + selectedHero.maxHealth.ToString() + "\n" + "\n";
            //desc += "Difficulty : " + "" + "\n";
            desc += "Max wave reached : " + Warrior.waveReached.ToString() + "\n";
            desc += Warrior.timesPlayed.ToString() + " games played";

            nameDescriptionText.GetComponent<TextMeshProUGUI>().text = Warrior.nameClass;
            descriptionText.GetComponent<TextMeshProUGUI>().text = desc;
            characterImage.sprite = warriorImage;
            if (Warrior.hasWon)
            {
                victoryIndicator.SetActive(true);
            }
            else
            {
                victoryIndicator.SetActive(false);
            }
        }
        if (i == 1) { 
            Mage selectedHero = new Mage();
            selectedString = "Mage";
            desc += "Health : " + selectedHero.maxHealth.ToString() + "\n" + "\n";
            //desc += "Difficulty : " + "" + "\n";
            desc += "Max wave reached : " + Mage.waveReached.ToString() + "\n";
            desc += Mage.timesPlayed.ToString() + " games played";
            
            nameDescriptionText.GetComponent<TextMeshProUGUI>().text = Mage.nameClass;
            descriptionText.GetComponent<TextMeshProUGUI>().text = desc;
            characterImage.sprite = mageImage;
            if (Mage.hasWon)
            {
                victoryIndicator.SetActive(true);
            }
            else
            {
                victoryIndicator.SetActive(false);
            }
        }
        if (i == 2)
        {
            Rogue selectedHero = new Rogue();
            selectedString = "Rogue";
            desc += "Health : " + selectedHero.maxHealth.ToString() + "\n" + "\n";
            //desc += "Difficulty : " + "" + "\n";
            desc += "Max wave reached : " + Rogue.waveReached.ToString() + "\n";
            desc += Rogue.timesPlayed.ToString() + " games played";

            nameDescriptionText.GetComponent<TextMeshProUGUI>().text = Rogue.nameClass;
            descriptionText.GetComponent<TextMeshProUGUI>().text = desc;
            characterImage.sprite = rogueImage;
            if (Rogue.hasWon)
            {
                victoryIndicator.SetActive(true);
            }
            else
            {
                victoryIndicator.SetActive(false);
            }
        }
    }
}
