using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    int currentWave = 0;
    float timeBetweenWaves = 5f;
    float spawnRate = 0.5f;
    bool canSpawnNextWave = true;

    public EnemyFactory enemyFactory;
    private InGameCanvas inGameCanvas;
    private PopupManager popupManager;

    // Start is called before the first frame update
    void Start()
    {
        inGameCanvas = GameObject.Find("InGameCanvas").GetComponent<InGameCanvas>();
        popupManager = FindObjectOfType<PopupManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (canSpawnNextWave && enemyFactory.enemyNumber == 0)
        {
            canSpawnNextWave = false;
            currentWave++;
            if (currentWave < 11)
            {
                StartCoroutine(SpawnWave(Fibonacci(currentWave)));
            }
            else
            {
                popupManager.DisplayVictoryPopup();
            }
        }
    }
    private int Fibonacci(int n)
    {
        if (n >= 2)
        {
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }
        if (n == 1) { return 1; }
        return 0;
    }

    IEnumerator SpawnWave(int enemyNumber)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        inGameCanvas.UpdateWaveDisplay(currentWave);
        int enemyCounter = 0;
        while(enemyCounter < enemyNumber)
        {
            if(enemyFactory.SpawnEnemy())
            {
                enemyCounter++;
            }
            yield return new WaitForSeconds(spawnRate);
        }
        canSpawnNextWave = true;
    }
}
