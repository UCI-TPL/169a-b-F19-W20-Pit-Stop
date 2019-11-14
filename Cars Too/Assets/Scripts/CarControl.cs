using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{

    public WheelCollider frontDriverCol, frontPassCol;
    public WheelCollider backDriverCol, backPassCol;

    public Transform frontDriver, frontPass;
    public Transform backDriver, backPass;

    public float steerAngle = 25.0f;
    public float motorForce = 1500f;

    public float turnAngle;

    float inputX, inputY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Inputs();

        Drive();

        SteerCar();

        UpdateAllWheels();
    }

    void Inputs()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }

    void Drive()
    {
        backDriverCol.motorTorque = inputY * motorForce;
        backPassCol.motorTorque = inputY * motorForce;
        frontDriverCol.motorTorque = inputY * motorForce;
        frontPassCol.motorTorque = inputY * motorForce;
    }

    void SteerCar()
    {
        turnAngle = steerAngle * inputX * -1;
        frontDriverCol.steerAngle = turnAngle;
        frontPassCol.steerAngle = turnAngle;
    }

    void UpdateWheelPos(WheelCollider col, Transform t)
    {
        Vector3 pos = t.position;
        Quaternion rot = t.rotation;

        col.GetWorldPose(out pos, out rot);
        t.position = pos;
        t.rotation = rot;
        t.transform.eulerAngles = new Vector3(rot.x, rot.y, -90);

    }

    void UpdateAllWheels()
    {
        UpdateWheelPos(frontDriverCol, frontDriver);
        UpdateWheelPos(frontPassCol, frontPass);
        //UpdateWheelPos(backDriverCol, backDriver);
        //UpdateWheelPos(backPassCol, backPass);

    }
}
