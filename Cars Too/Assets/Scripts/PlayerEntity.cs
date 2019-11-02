using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEntity : MonoBehaviour
{
    private Vector3 spawnPoint;
    private Vector3 spawnRot;

    [SerializeField] AudioClip pickup = null;
    bool boostUnlocked = false;
    bool hackUnlocked = false;
    bool jumpUnlocked = false;
    bool missilesUnlocked = false;

    

    void Start()
    {
        spawnPoint = this.transform.position;
        spawnRot = this.transform.localEulerAngles;
        Debug.Log(spawnPoint);
        Debug.Log(spawnRot);

    }

     void OnTriggerEnter(Collider other)
    {

        ProcessPickup(other);
        if(other.CompareTag("Floor"))
        {
            Respawn();
        }
    }
    public void ProcessPickup(Collider other)
    {
        
        if (other.CompareTag("CarPart"))
        {
            DataManager.instance.PickedUpCarPart(other.GetComponent<CarPart>());
            DataManager.instance.am.PlaySound(pickup);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("PresentBox"))
        {

            DataManager.instance.PickedUpGift(other.GetComponent<PresentBox>());
            DataManager.instance.am.PlaySound(pickup);
            Destroy(other.gameObject);
            //PrintGifts();
        }
    }

    public void Respawn()
    {
        //play some sounds or do something
        Debug.Log("Respawn");
        this.transform.position = spawnPoint;
        this.transform.localEulerAngles = spawnRot;
    }

    public void ChangeRespawnPoint(Vector3 newSpawn, Vector3 newRot)
    {
        spawnPoint = newSpawn;
        spawnRot = newRot;
    }
    
}
