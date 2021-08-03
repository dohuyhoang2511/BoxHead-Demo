using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGasTankHealth : MonoBehaviour
{
    float gasTankHealth = 10;
    public int dame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gasTankHealth <= 0)
        {
            gameObject.GetComponent<GasTank>().Explosion(dame = 100);
        }    
    }
    public void GotHit(int dame)
    {
        gasTankHealth -= dame;
    }
}
