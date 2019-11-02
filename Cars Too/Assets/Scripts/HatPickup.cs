using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatPickup : MonoBehaviour
{
    public PlayerEntity player;
    // Start is called before the first frame update
    void Awake()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEntity>();   
    }
    private void OnTriggerEnter(Collider other)
    {
        player.ProcessPickup(other);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
