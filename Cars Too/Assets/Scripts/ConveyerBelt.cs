using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private Transform endpoint;
    [SerializeField] private bool player = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!player)
            {
                
                if (other.transform.parent != null)
                {
                    
                    other.transform.parent.transform.parent.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.position, speed * Time.deltaTime);
                    player = true;
                }
                else {
                    other.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.position, speed * Time.deltaTime);
                    player = true;
                }
            }
        }
        else
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.position, speed * Time.deltaTime);
        }
    }

    private void Update()
    {
        player = false;
    }

}
