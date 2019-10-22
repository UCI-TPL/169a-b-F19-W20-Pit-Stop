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

            Destroy(other.gameObject);
        }

        if(other.CompareTag("PresentBox"))
        {
            PresentType present = other.GetComponent<PresentBox>().GetPresentType();

            DataManager.instance.AddGift(present,1);

            Destroy(other.gameObject);
            //PrintGifts();
        }
    }
    
}
