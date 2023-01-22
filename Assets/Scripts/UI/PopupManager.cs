using UnityEngine.SceneManagement;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    private GameObject victoryPopupCanvas;
    private GameObject gameOverPopupCanvas;
    private GameObject upgradePopupCanvas;

    // Start is called before the first frame update
    void Start()
    {
        victoryPopupCanvas = GameObject.Find("VictoryPopupCanvas");
        gameOverPopupCanvas = GameObject.Find("GameOverPopupCanvas");
        upgradePopupCanvas = GameObject.Find("UpgradePopupCanvas");
        victoryPopupCanvas.SetActive(false);
        gameOverPopupCanvas.SetActive(false);
        upgradePopupCanvas.SetActive(false);
        DisplayVictoryPopup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayVictoryPopup()
    {
        Time.timeScale = 0;
        victoryPopupCanvas.SetActive(true);
    }

    public void DisplayGameOverPopup()
    {
        Time.timeScale = 0;
        gameOverPopupCanvas.SetActive(true);
    }
    public void DisplayUpgradePopup()
    {
        // Plein de choses à faire là
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
        //UpgradeManager
        upgradePopupCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
