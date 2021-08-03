using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosion;
    private float radius = 2.0f;
    private float timer;
    private float explosionTime;
    private bool hasExploded;
    public int dame;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f; 
        explosionTime = 2.0f;
        hasExploded = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= explosionTime)
        {
            if (hasExploded == false)
            {
                Vector3 explosionPos = gameObject.transform.position;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
                for (int i = 0; i < colliders.Length; i++)
                {
                    GameObject objectTargeted = colliders[i].gameObject;
                    if (objectTargeted.tag == "orc")
                        objectTargeted.GetComponent<ManageOrcHealth>().GotHit(dame);
                    if (objectTargeted.tag == "boss")
                        objectTargeted.GetComponent<ManageBossHealth>().GotHit(dame);
                    if (objectTargeted.tag == "gas_tank")
                        objectTargeted.GetComponent<ManageGasTankHealth>().GotHit(dame);
                    if (objectTargeted.tag == "weapon_wall")
                        objectTargeted.GetComponent<ManageWallHealth>().GotHit(dame);
                }
            }
            GameObject.Instantiate(explosion, transform.position, Quaternion.identity);

            hasExploded = true;
            Destroy(gameObject);
            timer = 0;
        }
    }
}

