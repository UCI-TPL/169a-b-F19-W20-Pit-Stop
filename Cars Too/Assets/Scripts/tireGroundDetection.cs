using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tireGroundDetection : MonoBehaviour
{
    
    public bool isGrounded;
    [SerializeField] private Collider tireCollider;
    
    // Start is called before the first frame update
    void Awake()
    {
        isGrounded = false;
        tireCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerStay(Collider other) {
        if(!other.name.ToLower().Contains("trigger")) {
            isGrounded = true;
        }
        
    }

    private void OnTriggerExit(Collider other) {
        isGrounded = false;
    }

}
