using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PresentType {one, two, three, four, five};

public class PresentBox : Id
{
    [SerializeField] int value = 1;
    [SerializeField] PresentType myPresentType;
    [SerializeField] List<GameObject> presentmodels;
    private bool collected = false;

    private int numberOfPresentTypes = 5;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        int type = Random.Range(0, numberOfPresentTypes-1);
        if (type == 2)
        {
            type = 4;
        }
        myPresentType = (PresentType) type;
        //Debug.Log(myPresentType);
        Instantiate(presentmodels[(int)myPresentType], this.transform);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PresentType GetPresentType()
    {
        return myPresentType;
    }

    public int GetValue()
    {
        return value;
    }

    public bool GetCollected()
    {
        return collected;
    }

    public void CollectGift()
    {
        collected = true;
    }
}
