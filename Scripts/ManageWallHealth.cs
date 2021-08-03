using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWallHealth : MonoBehaviour
{
    float wallHealth = 200f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wallHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void GotHit(int dame)
    {
        wallHealth -= dame;
    }
}
