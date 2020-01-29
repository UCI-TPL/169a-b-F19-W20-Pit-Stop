﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventTriggerStatic : Id
{
    private static int partscollected = 0;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        DataManager.instance.carPartAcquired.AddListener(IncrementParts);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IncrementParts()
    {
        partscollected++;
        if (partscollected >= 2)
        {
            DataManager.instance.AddID(GetID());
            SceneManager.LoadScene("Event3");
        }
    }
}