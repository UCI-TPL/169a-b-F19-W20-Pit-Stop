using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEntity : MonoBehaviour
{


    bool boostUnlocked = false;
    bool hackUnlocked = false;
    bool jumpUnlocked = false;
    bool missilesUnlocked = false;

    

    void Start()
    {
      

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CarPart"))
        {
            DataManager.instance.AddCarParts(other.GetComponent<CarPart>().GetValue());
            DataManager.instance.AddCarPartID(other.GetComponent<CarPart>().GetID());

            Destroy(other.gameObject);
        }

        if(other.CompareTag("PresentBox"))
        {
            PresentType present = other.GetComponent<PresentBox>().GetPresentType();

            DataManager.instance.AddGift(present,1);
            DataManager.instance.AddGiftID(other.GetComponent<PresentBox>().GetID());

            Destroy(other.gameObject);
            //PrintGifts();
        }
    }
    
}
