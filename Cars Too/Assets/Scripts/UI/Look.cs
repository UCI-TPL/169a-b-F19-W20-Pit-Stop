using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Causes an Object to always rotate towards a target
//Defaults to camera
public class Look : MonoBehaviour
{
    public Transform target =null;
    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindObjectOfType<Camera>().transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + target.rotation * Vector3.forward, target.rotation * Vector3.up);
    }
}
