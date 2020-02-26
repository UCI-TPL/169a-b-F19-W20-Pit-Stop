using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWheels : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody playerrb;
    [SerializeField] float basespeed = 0.5f;
    private Vector3 forward;
    private Vector3 oldpos;

    void Start()
    {
        forward = playerrb.transform.forward;
        oldpos = playerrb.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mov = playerrb.transform.position - oldpos;
        if (Vector3.Dot(forward,mov)<0)
        {
            transform.Rotate(0, -basespeed * playerrb.velocity.magnitude, 0);
        }
        else
        {
            transform.Rotate(0, basespeed * playerrb.velocity.magnitude, 0);
        }

        forward = playerrb.transform.forward;
        oldpos = playerrb.transform.position;
    }
}
