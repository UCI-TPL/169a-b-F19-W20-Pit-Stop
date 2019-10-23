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
            DataManager.instance.PickedUpCarPart(other.GetComponent<CarPart>());

            Destroy(other.gameObject);
        }

        if(other.CompareTag("PresentBox"))
        {

            DataManager.instance.PickedUpGift(other.GetComponent<PresentBox>());
            Destroy(other.gameObject);
            //PrintGifts();
        }
    }
    
}
