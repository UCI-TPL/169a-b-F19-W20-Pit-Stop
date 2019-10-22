using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;

    
    Dictionary<string, int> confidantExp = new Dictionary<string, int>();

    Dictionary<PresentType, int> gifts = new Dictionary<PresentType, int>();

    [SerializeField] public int carParts=0;

    public UnityEvent giftAcquired;
    public UnityEvent carPartAcquired;

    // Start is called before the first frame update
    void Awake()
    {
        //Dont destory this gameobject between scenes
        DontDestroyOnLoad(this.gameObject);

        //Destroy this object if there is already another one
        if (instance!=this&&instance!=null)
        {
            Destroy(this.gameObject);
        }
        if (instance == null)
        {
            instance = this;

            confidantExp["Piper"] = 0;
            confidantExp["Dex"] = 0;
            confidantExp["Loco"] = 0;
            confidantExp["Springtrap"] = 0;
            confidantExp["Mustang"] = 0;

            foreach (PresentType present in PresentType.GetValues(typeof(PresentType)))
            {
                gifts.Add(present, 0);
            }
        }
    }

    public int GetGiftCount(PresentType present)
    {
        return gifts[present];
    }

    //returns the number of carparts
    public int GetCarParts()
    {
        return carParts;
    }

    public void AddCarParts(int i)
    {
        carParts += i;
        carPartAcquired.Invoke();
    }

    //returns the Exp of a given confidant
    public int GetConfidantEXP(string confidant)
    {
        return confidantExp[confidant];
    }

    //Adds Gifts of a given amount to the present type
    public void AddGift(PresentType present, int i)
    {
        
        gifts[present] += i;
        giftAcquired.Invoke();
    }

    //Subtracts values from a gift of a given type
    //Returns false if not possible and doesnt subtract e.g. it would result in negative gifts
    public bool SubtractGift(PresentType present, int i)
    {
        if (GetGiftCount(present)>=i)
        {
            gifts[present] -= i;
            return true;
        }
        return false;
    }

    private void PrintGifts()
    {
        //Prints out key and values
        foreach (KeyValuePair<PresentType, int> gift in gifts)
        {
            Debug.LogFormat("PresentType = {0}, Value = {1}", gift.Key, gift.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
