using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameobject : MonoBehaviour
{
    [SerializeField] private GameObject triggerobject; //gameobject to be turned on or off
    [SerializeField] private bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerobject.SetActive(!triggerobject.activeSelf);
            if(once)
                 Destroy(this);//only triggers once
        }
    }
}
