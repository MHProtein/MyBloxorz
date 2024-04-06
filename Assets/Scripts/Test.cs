using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject marker1; 
    void Start()
    {
        
    }


    void Update()
    {
        transform.RotateAround(marker1.transform.position, Vector3.left, 90.0f);
    }
}
