﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
