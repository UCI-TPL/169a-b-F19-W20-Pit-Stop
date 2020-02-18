using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private Transform endpoint;
    [SerializeField] private bool player = false;
    Transform playerTransform = null;

    private void OnTriggerStay(Collider other)
    {
        // if (other.CompareTag("Player"))
        // {
        //     if (!player)
        //     {
                
        //         if (other.transform.parent != null)
        //         {
                    
        //             other.transform.parent.transform.parent.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.position, speed * Time.deltaTime);
        //             player = true;
        //         }
        //         else {
        //             other.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.position, speed * Time.deltaTime);
        //             player = true;
        //         }
        //     }
        // }
        // else
        // {
        //     other.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.position, speed * Time.deltaTime);
        // }

        //if(playerTransform == null) {
        //    if(other.transform.root.CompareTag("Player")) {
        //        playerTransform = other.transform.root;
        //    }
        //} else {
        //    playerTransform.position = Vector3.MoveTowards(playerTransform.position, endpoint.position, speed * Time.deltaTime);
        //}

        if(other.CompareTag("Player"))
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.position, speed * Time.deltaTime * 0.25f);
        }

        if(other.name == "Box(Clone)") {
            other.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.position, speed * Time.deltaTime);
        }
    }

    //private void OnTriggerExit(Collider other) {
    //    playerTransform = null;
    //}

    private void Update()
    {
        player = false;
    }

}
