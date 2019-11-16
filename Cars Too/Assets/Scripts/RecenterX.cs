using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RecenterX : MonoBehaviour
{
    [SerializeField] private float timer = 3.0f; //inactive mousetime before recentering 
    [SerializeField] private float currenttime = 0.0f; //the curent time w/o mousemovement
    [SerializeField] private CinemachineFreeLook cfl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mouse moved 
        if(Input.GetAxis("Mouse X")==0&& Input.GetAxis("Mouse Y")== 0) 
        {
            currenttime += Time.deltaTime;
            if (currenttime >= timer)
            {
                cfl.m_RecenterToTargetHeading.m_enabled = true;

            }
        }
        else
        {
            //if moved, reset timer and turn off recenter
            currenttime = 0.0f;
            cfl.m_RecenterToTargetHeading.m_enabled = false;
        }
    }
}
