using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    int currentWave = 0;
    float timeBetweenWaves = 5f;
    float spawnRate = 0.5f;
    bool stopSpawning = false;

    public EnemyFactory enemyFactory;
    private InGameCanvas inGameCanvas;


    // Start is called before the first frame update
    void Start()
    {
        inGameCanvas = GameObject.Find("InGameCanvas").GetComponent<InGameCanvas>();
        StartCoroutine(SpawnWave(10));
    }

    // Update is called once per frame
    void Update()
    {
        inGameCanvas.UpdateWaveDisplay(currentWave);
    }



    IEnumerator SpawnWave(int enemyNumber)
    {
        int enemyCounter = 0;
        while(enemyCounter < enemyNumber)
        {
            if(enemyFactory.SpawnEnemy())
            {
                enemyCounter++;
            }
            yield return new WaitForSeconds(spawnRate);

        }
        
    }
}
