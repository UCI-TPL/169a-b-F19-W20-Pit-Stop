using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPart : Id
{
    
    [SerializeField] int value;
    private bool collected = false;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetValue()
    {
        return value;
    }

    public bool GetCollected()
    {
        return collected;
    }

    public void CollectPart()
    {
        collected = true;
    }

   
}
