using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using TMPro;

public class PopupManager : MonoBehaviour
{
    private GameObject victoryPopupCanvas;
    private GameObject gameOverPopupCanvas;
    private GameObject upgradePopupCanvas;

    private TextMeshProUGUI popupTitleText1;
    private TextMeshProUGUI popupDesccriptionText1;
    private TextMeshProUGUI popupTitleText2;
    private TextMeshProUGUI popupDesccriptionText2;
    private TextMeshProUGUI popupTitleText3;
    private TextMeshProUGUI popupDesccriptionText3;
    private TextMeshProUGUI deathWaveText;
    private TextMeshProUGUI deathTimeText;
    private TextMeshProUGUI victoryTimeText;

    Hero player;

    private InGameCanvas inGameCanvas;
    UpgradesManager upgradeManager;
    Tuple<string, string>[] possibleUpgrades;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Hero>();

        inGameCanvas = GameObject.Find("InGameCanvas").GetComponent<InGameCanvas>();

        popupTitleText1 = GameObject.Find("TitleText1").GetComponent<TextMeshProUGUI>();
        popupDesccriptionText1 = GameObject.Find("DescriptionText1").GetComponent<TextMeshProUGUI>();
        popupTitleText2 = GameObject.Find("TitleText2").GetComponent<TextMeshProUGUI>();
        popupDesccriptionText2 = GameObject.Find("DescriptionText2").GetComponent<TextMeshProUGUI>();
        popupTitleText3 = GameObject.Find("TitleText3").GetComponent<TextMeshProUGUI>();
        popupDesccriptionText3 = GameObject.Find("DescriptionText3").GetComponent<TextMeshProUGUI>();

        deathWaveText = GameObject.Find("GameOverWaveText").GetComponent<TextMeshProUGUI>();
        deathTimeText = GameObject.Find("GameOverTimeText").GetComponent<TextMeshProUGUI>();
        victoryTimeText = GameObject.Find("VictoryTimeText").GetComponent<TextMeshProUGUI>();

        victoryPopupCanvas = GameObject.Find("VictoryPopupCanvas");
        gameOverPopupCanvas = GameObject.Find("GameOverPopupCanvas");
        upgradePopupCanvas = GameObject.Find("UpgradePopupCanvas");
        victoryPopupCanvas.SetActive(false);
        gameOverPopupCanvas.SetActive(false);
        upgradePopupCanvas.SetActive(false);

        upgradeManager = GameObject.Find("UpgradesManager").GetComponent<UpgradesManager>();
    }

    public void DisplayVictoryPopup()
    {
        Time.timeScale = 0;
        float time = inGameCanvas.timer;
        int minutes = Mathf.FloorToInt(time / 60);
        int secondes = Mathf.FloorToInt(time % 60);
        victoryTimeText.text = "Final Time : " + String.Format("{0}:{1}", minutes.ToString("D2"), secondes.ToString("D2"));

        victoryPopupCanvas.SetActive(true);
    }

    public void DisplayGameOverPopup()
    {
        Time.timeScale = 0;
        float time = inGameCanvas.timer;
        int minutes = Mathf.FloorToInt(time / 60);
        int secondes = Mathf.FloorToInt(time % 60);
        deathTimeText.text = "Final Time : " + String.Format("{0}:{1}", minutes.ToString("D2"), secondes.ToString("D2"));

        int wave = inGameCanvas.waveNum - 1;
        deathWaveText.text = "Wave Killed : " + wave.ToString();

        gameOverPopupCanvas.SetActive(true);
    }
    public void DisplayUpgradePopup()
    {
        possibleUpgrades = upgradeManager.GetPossibleUpgrades();

        popupTitleText1.text = possibleUpgrades[0].Item1;
        popupDesccriptionText1.text = possibleUpgrades[0].Item2;
        popupTitleText2.text = possibleUpgrades[1].Item1;
        popupDesccriptionText2.text = possibleUpgrades[1].Item2;
        popupTitleText3.text = possibleUpgrades[2].Item1;
        popupDesccriptionText3.text = possibleUpgrades[2].Item2;

        Time.timeScale = 0;
        upgradePopupCanvas.SetActive(true);
    }

    public void SwitchToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void HideUpgradePopup(int upgradeNum)
    {
        // player.ApplyUpgrade(possibleUpgrades[upgradeNum].Item1);
        Debug.Log("Upgrade : " + possibleUpgrades[upgradeNum].Item1);
        upgradePopupCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
