using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTank : MonoBehaviour
{
    public GameObject explosion;
    private float radius = 5.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Explosion(int dame)
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
        GameObject.Instantiate(explosion, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
