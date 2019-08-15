using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    Rigidbody rb;
    public float deadZone = 0.1f;
    public float m_groundedDrag = 3f;
    public float maxVelocity = 50f;
    public float hoverForce = 1000f;
    public float gravityForce = 1000f;
    public float hoverHeight = 1.5f;
    public GameObject[] hoverPoints;

    public float forwardAcceleration = 8000f;
    public float reverseAcceleration = 4000f;
    float thrust = 0f;

    public float turnStrength = 1000f;
    float turnValue = 0f;

    int layerMask;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.down;

        layerMask = 1 << LayerMask.NameToLayer("Vehicle");
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        //Thrust forward
        thrust = 0.0f;
        float acceleration = Input.GetAxis("Vertical");
        if (acceleration > deadZone)
        {
            thrust = acceleration * forwardAcceleration;
        }
        else if (acceleration < -deadZone)
        {
            thrust = acceleration * reverseAcceleration;
        }

        //Turning mechanics
        turnValue = 0.0f;
        float turnAxis = Input.GetAxis("Horizontal");
        if (Mathf.Abs(turnAxis) > deadZone)
        {
            turnValue = turnAxis;
        }
    }

    void FixedUpdate()
    {
        //Add a force on all four hover corners of the car that allows the car to hover/simulates normal force and gravity
        RaycastHit hit;
        bool grounded = false;
        for (int i = 0; i < hoverPoints.Length; i++)
        {
            var hoverPoint = hoverPoints[i];
            if (Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out hit, hoverHeight, layerMask))
            {
                //More force is applied the closer the car is to the ground to stablize the car
                rb.AddForceAtPosition(Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
                grounded = true;
            }
            else
            {
                //Adds forces to the hover points to stabilize the car when it is in the air
                if (transform.position.y > hoverPoint.transform.position.y)
                {
                    rb.AddForceAtPosition(hoverPoint.transform.up * gravityForce, hoverPoint.transform.position);
                }
                else
                {
                    rb.AddForceAtPosition(hoverPoint.transform.up * -gravityForce, hoverPoint.transform.position);
                }
            }
        }

        //Changes the drag of vehicle based on whether its grounded or not
        if (grounded)
        {
            rb.drag = m_groundedDrag;
        }
        else
        {
            rb.drag = 0.1f;
            //Also limit the thrust/turn of the vehicle when in the air
            thrust /= 100f;
            turnValue /= 100f;
        }


        //Adding the force to simulate forward and reverse thrust
        if (Mathf.Abs(thrust) > 0)
            rb.AddForce(transform.forward * thrust);

        //When turning add relative torque to pivot/turn the car
        if (turnValue > 0 || turnValue < 0)
        {
            rb.AddRelativeTorque(Vector3.up * turnValue * turnStrength);
        }

        //Limit the velocity to a maximum
        if (rb.velocity.sqrMagnitude > (rb.velocity.normalized * maxVelocity).sqrMagnitude)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }

    }
}
