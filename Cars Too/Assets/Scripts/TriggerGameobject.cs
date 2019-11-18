using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameobject : MonoBehaviour
{
    [SerializeField] private GameObject triggerobject; //gameobject to be turned on or off
    [SerializeField] private bool once = false;
    [SerializeField] private bool triggerablebyhat = false; //determines if the trigger can be set off by the player's hat.
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
        //if the player, or the player's hat enters the trigger box flip the gameobjects active state.
        if ((other.CompareTag("Player")&&!once)||(other.CompareTag("PlayerHat") && !once&&triggerablebyhat))
        {
            triggerobject.SetActive(!triggerobject.activeSelf);
            once = true;

            
        }
    }

}
