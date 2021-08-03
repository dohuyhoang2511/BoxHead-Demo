﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject cube;
    public GameObject notWall;
    private int[,] createMap = new int[,]
        {
            {1,1,1,2,1,1,1,1,1,1,1,1,1,2,1,1,1,1,1 },
            {1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,1 },
            {1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,1 },
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1 },
            {1,0,1,1,1,0,1,1,1,1,1,1,1,0,1,0,0,0,1 },
            {1,0,1,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,1 },
            {1,0,1,0,1,0,1,1,1,1,1,0,1,1,1,1,1,0,1 },
            {1,0,0,0,1,0,1,0,0,0,1,0,0,0,0,0,0,0,1 },
            {1,0,0,0,1,0,1,1,1,0,1,1,1,1,1,1,1,0,1 },
            {1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,1 },
            {1,1,1,0,1,1,1,0,0,0,1,1,1,1,1,0,1,0,1 },
            {1,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,1,0,1 },
            {1,0,0,0,1,1,1,0,0,0,1,0,0,0,1,0,1,0,1 },
            {1,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1 },
            {1,1,1,1,1,1,1,0,0,0,1,0,1,0,1,0,1,0,1 },
            {1,0,0,0,0,0,0,0,0,0,1,0,1,0,1,0,1,0,1 },
            {1,0,0,0,1,1,1,1,1,1,1,0,1,0,1,0,1,0,1 },
            {1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,1 },
            {1,0,0,0,0,0,1,1,1,0,0,0,1,0,1,1,1,0,1 },
            {1,0,0,0,0,0,1,0,0,0,0,0,1,0,1,0,0,0,1 },
            {1,1,1,1,1,0,1,0,1,1,1,1,1,0,1,0,0,0,1 },
            {2,0,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,1 },
            {1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,2,1 }
        };
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 23; i++)
        {
            for(int j = 0; j < 19; j++)
            {
                if (createMap[i,j] == 1)
                {
                    GameObject t = (GameObject)(Instantiate(cube, new Vector3(11f - i, 1.0f,9f - j), Quaternion.identity));
                }
            }    
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
