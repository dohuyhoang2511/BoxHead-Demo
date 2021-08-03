using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    int dame = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameObject objectTarget = other.gameObject;
            objectTarget.GetComponent<ManagePlayerHealth>().GotHit(dame);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "orc")
        {
            GameObject objectTarget = other.gameObject;
            objectTarget.GetComponent<ManageOrcHealth>().GotHit(dame);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "weapon_wall")
        {
            GameObject objectTarget = other.gameObject;
            objectTarget.GetComponent<ManageWallHealth>().GotHit(dame);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "gas_tank")
        {
            GameObject objectTarget = other.gameObject;
            objectTarget.GetComponent<ManageGasTankHealth>().GotHit(dame);
            Destroy(gameObject);
        }
        print("Trigger " + other.gameObject.tag);
    }
}
