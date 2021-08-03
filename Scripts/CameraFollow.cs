using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private float minX = -7.5f;
    private float maxX = 8f;
    private float minZ = -10f;
    private float maxZ = 2f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            Vector3 temp = transform.position;
            temp.x = player.position.x;
            temp.z = player.position.z - 3.2f;
            if(temp.x < minX)
            {
                temp.x = minX;
            }    
            if(temp.x > maxX)
            {
                temp.x = maxX;
            }    
            if(temp.z < minZ)
            {
                temp.z = minZ;
            }    
            if(temp.z > maxZ)
            {
                temp.z = maxZ;
            }    
            transform.position = temp;
        }    
    }
}
