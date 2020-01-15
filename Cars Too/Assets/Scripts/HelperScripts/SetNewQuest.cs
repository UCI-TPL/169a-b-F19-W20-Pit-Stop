using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNewQuest : Id
{
    [SerializeField] private int newpartsneeded = 8;
    //Used to Set the New Objective number of parts at the beginning of the level.
    private void Awake()
    {
        base.Start();
        GameObject.FindObjectOfType<ObjectiveTracking>().SetPartsNeeded(newpartsneeded);
        DataManager.instance.AddID(GetID());
    }
    public override void Start()
    {
        //Only do this once
        //base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
