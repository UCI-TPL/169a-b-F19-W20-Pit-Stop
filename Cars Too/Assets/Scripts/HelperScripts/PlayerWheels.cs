using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWheels : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody playerrb;
    [SerializeField] float basespeed = 5.0f;
    private Vector3 forward;
    private Vector3 oldpos;
    [SerializeField] bool forwardwheel = false;
    // Maximum turn rate in degrees per second.
    public float turningRate = 30f;
    // Rotation we should blend towards.
    private Quaternion _targetRotation = Quaternion.identity;
    [SerializeField] private GameObject leftrot;
    [SerializeField] private GameObject baserot;
    [SerializeField] private GameObject rightrot;

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
            transform.Rotate(0, -basespeed * playerrb.velocity.magnitude*Time.deltaTime, 0);
        }
        else
        {
            transform.Rotate(0, basespeed * playerrb.velocity.magnitude*Time.deltaTime, 0);
        }

        forward = playerrb.transform.forward;
        oldpos = playerrb.transform.position;

        if (forwardwheel)
        {
            TurnWheels();
        }
    }

    // Call this when you want to turn the object smoothly.
    public void SetBlendedEulerAngles(Quaternion q)
    {
        _targetRotation = q;
    }

    void TurnWheels()
    {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            SetBlendedEulerAngles(leftrot.transform.rotation);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, turningRate * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            SetBlendedEulerAngles(rightrot.transform.rotation);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, turningRate * Time.deltaTime);
        }

         if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            SetBlendedEulerAngles(baserot.transform.rotation);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, turningRate * Time.deltaTime);
        }
    }



}
