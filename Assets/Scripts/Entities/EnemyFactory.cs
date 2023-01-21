using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    private GameObject[] enemyTypes;

    private GameObject[] spawnPoints;

    public int enemyNumber = 0;
    public int maxEnemyNumber = 500;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        enemyTypes = new GameObject[] { enemy1, enemy2, enemy3 };
    }

    // return true if the enemy is spawned
    public bool SpawnEnemy()
    {
        if (enemyNumber >= maxEnemyNumber) { return false; };
        Vector3 spawnPosition = selectSpawnPosition();
        GameObject newEnemy = Instantiate(selectEnemy(), spawnPosition, Quaternion.identity);
        enemyNumber++;
        return true;
    }

    private Vector3 selectSpawnPosition()
    {
        GameObject selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        if (selectedSpawnPoint == null) { return new Vector3(0, 1, 0); }
        Vector3 currentPoint = selectedSpawnPoint.transform.position;
        currentPoint.y += .5f;
        return currentPoint;
    }
    private GameObject selectEnemy()
    {
        GameObject selectedEnemy = enemyTypes[Random.Range(0, enemyTypes.Length)];
        return selectedEnemy;
    }

    public void decreaseEnemyCounter()
    {
        enemyNumber -= 1;
        if (enemyNumber < 0) { enemyNumber = 0; }
    }
}
