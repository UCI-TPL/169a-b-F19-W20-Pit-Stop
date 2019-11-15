using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    PlayerEntity player;
    Rigidbody rb;
    int layerMask;

    [Header("Car variables")]
    public float deadZone = 0.1f;
    public float m_groundedDrag = 3f;
    public float maxVelocity = 50f;
    public float hoverForce = 1000f;
    public float gravityForce = 1000f;
    public float hoverHeight = 1.5f;
    public GameObject[] hoverPoints;

    public float forwardAcceleration = 8000f;
    public float reverseAcceleration = 4000f;
    [SerializeField] float thrust = 0f;

    public float turnStrength = 1000f;
    [SerializeField] float turnValue = 0f;

    [Header("When in air variables")]
    [SerializeField] float airThrustLoss = 100f;
    [SerializeField] float airTurnLoss = 100f;
    [SerializeField] bool airStabilize = true;


    [Header("Boost variables")]
    [SerializeField] float currentBoostSpeed = 0f;
    [SerializeField] float maxBoostSpeed = 2000f;
    [SerializeField] float boostFactor;

    [Header("Bools")]
    [SerializeField] bool isGrounded = false;
    [SerializeField] bool isPaused = false;
    [SerializeField] bool boostUnlocked = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.down;

        layerMask = 1 << LayerMask.NameToLayer("Vehicle");
        layerMask = ~layerMask;

        player = this.GetComponent<PlayerEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPaused) {
            //Respawn
            if(Input.GetKeyDown(KeyCode.R))
            {
                player.Respawn();
            }

            Brake();

            //Boost
            if(boostUnlocked)
                Boost();
           
            //Thrust forward
            thrust = 0.0f;

            float acceleration = Input.GetAxis("Vertical");
            if (acceleration > deadZone)
            {
                thrust = (acceleration * forwardAcceleration) + currentBoostSpeed;
            }
            else if (acceleration < -deadZone)
            {
                thrust = acceleration * reverseAcceleration;
            }

            Brake();

            //Turning mechanics
            turnValue = 0.0f;
            float turnAxis = Input.GetAxis("Horizontal");
            if (Mathf.Abs(turnAxis) > deadZone)
            {
                turnValue = turnAxis;
            }
        }
    }

    void FixedUpdate()
    {
        if(!isPaused) {
            //Add a force on all four hover corners of the car that allows the car to hover/simulates normal force and gravity
            RaycastHit hit;
            isGrounded = false;
            for (int i = 0; i < hoverPoints.Length; i++)
            {
                var hoverPoint = hoverPoints[i];
                if (Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out hit, hoverHeight, layerMask))
                {
                    //More force is applied the closer the car is to the ground to stablize the car
                    rb.AddForceAtPosition(Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
                    isGrounded = true;
                }
                else
                {
                    if(airStabilize)
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
            }

            //Changes the drag of vehicle based on whether its grounded or not
            if (isGrounded)
            {
                rb.drag = m_groundedDrag;
            }
            else
            {
                rb.drag = 0.1f;
                //Also limit the thrust/turn of the vehicle when in the air
                thrust /= airThrustLoss;
                turnValue /= airTurnLoss;
            }


            //Adding the force to simulate forward and reverse thrust
            if (Mathf.Abs(thrust) > deadZone)
            {
                rb.AddForce(transform.forward * thrust);

                if(thrust > 0)
                {
                    rb.AddRelativeTorque(Vector3.up * turnValue * turnStrength);
                    //Debug.Log("Forward: " + turnValue * turnStrength);
                }
                else
                {
                    rb.AddRelativeTorque(Vector3.down * turnValue * turnStrength );
                    //Debug.Log("Backward: " + turnValue * turnStrength);
                }
                    
            }

            //When turning add relative torque to pivot/turn the car
            
            if ((turnValue > 0 || turnValue < 0) && thrust > -deadZone && thrust < deadZone)
            {
                //rb.AddForce(transform.forward * 3000f);
                rb.AddRelativeTorque(Vector3.up * turnValue * turnStrength);
            } 

            //Limit the velocity to a maximum
            if (rb.velocity.sqrMagnitude > (rb.velocity.normalized * maxVelocity).sqrMagnitude)
            {
                rb.velocity = rb.velocity.normalized * maxVelocity;

            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        rb.velocity = Vector3.zero;
    }

    public void Unpause()
    {
        isPaused = false;
    }

    public void Boost()
    {
        
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            //Debug.Log("boost");
            if (currentBoostSpeed < maxBoostSpeed)
                currentBoostSpeed += boostFactor;
        }
        else
        {
            if (currentBoostSpeed > 0)
                currentBoostSpeed -= boostFactor;
            if (currentBoostSpeed < 0)
                currentBoostSpeed = 0;
        }
    }

    public void UnlockBoost()
    {
        boostUnlocked = true;
    }

    public void LockBoost()
    {
        boostUnlocked = false;
    }

    public void Brake()
    {
        //Debug.Log("Braking");
        if(Input.GetKey(KeyCode.Space))
        {
            //rb.velocity = Vector3.zero;
            thrust = 0;
        }
    }
}
