using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject orc;
    public GameObject boss;

    private float startTimeSpawnOrc;
    private float startTimeSpawnBoss;
    private int numberOrcHadSpawn = 0;
    private int numberBossHadSpawn = 0;

    float timer;

    private GameObject[] spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0f)
        {
            UpdateSpawn(10, 5, 5, 10);
        }
        else if(timer > 50f)
        { 
            UpdateSpawn(20, 10, 3, 7);
        }
        else if (timer > 120f)
        {
            UpdateSpawn(50, 15, 2, 5);
        }
        else if (timer > 220f)
        {
            UpdateSpawn(100, 50, 1, 3);
        }
        else if (timer > 370f)
        {
            UpdateSpawn(1000, 200, 2, 5);
        }
    }
    public void UpdateSpawn(int numberOrc, int numberBoss, float timeSpawnOrc, float timeSpawnBoss)
    {
        startTimeSpawnOrc += Time.deltaTime;
        startTimeSpawnBoss += Time.deltaTime;
        if(numberOrcHadSpawn <= numberOrc)
        {
            if (startTimeSpawnOrc > timeSpawnOrc)
            {
                SpawnOrc();
                numberOrcHadSpawn++;
                startTimeSpawnOrc = 0;
            }

        }
        if(numberBossHadSpawn <= numberBoss)
        {
            if (startTimeSpawnBoss > timeSpawnBoss)
            {
                SpawnBoss();
                numberBossHadSpawn++;
                startTimeSpawnBoss = 0;
            }
        }
    }
    public void SpawnOrc()
    {
        int r = Random.Range(0, spawnPoint.Length);
        Instantiate(orc, spawnPoint[r].transform.position, Quaternion.identity);    
    }  
    public void SpawnBoss()
    {
        int r = Random.Range(0, spawnPoint.Length);
        Instantiate(boss, spawnPoint[r].transform.position, Quaternion.identity);
    }
}
