using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEntity : MonoBehaviour
{
    private Vector3 spawnPoint;

    [SerializeField] AudioClip pickup = null;
    bool boostUnlocked = false;
    bool hackUnlocked = false;
    bool jumpUnlocked = false;
    bool missilesUnlocked = false;

    

    void Start()
    {
        spawnPoint = this.transform.position;

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CarPart"))
        {
            DataManager.instance.PickedUpCarPart(other.GetComponent<CarPart>());
            DataManager.instance.am.PlaySound(pickup);
            Destroy(other.gameObject);
        }

        if(other.CompareTag("PresentBox"))
        {

            DataManager.instance.PickedUpGift(other.GetComponent<PresentBox>());
            DataManager.instance.am.PlaySound(pickup);
            Destroy(other.gameObject);
            //PrintGifts();
        }

        if(other.CompareTag("Floor"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        //play some sounds or do something

        this.transform.position = spawnPoint;
    }

    public void ChangeRespawnPoint(Vector3 newSpawn)
    {
        spawnPoint = newSpawn;
    }
    
}
