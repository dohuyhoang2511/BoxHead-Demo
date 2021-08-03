using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAmmoPack : MonoBehaviour
{
    public GameObject ammoPack;
    //GameObject[] spawnPoint;

    float timer;
    bool isCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        SpawnAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawnAmmo();
    }
    void UpdateSpawnAmmo()
    {
        int r = Random.Range(5, 20);
        timer += Time.deltaTime;
        if(timer > 15f && isCollected)
        {
            SpawnAmmo();
            timer = 0;
        }
    }
    void SpawnAmmo()
    {
        Instantiate(ammoPack, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isCollected = true;
        }
    }
}
