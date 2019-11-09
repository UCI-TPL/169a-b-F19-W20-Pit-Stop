using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMovement : MonoBehaviour
{
    //rotate speed
    public float turnSpeed = 60.0f;

    //bobbing effect
    public float amplitude = 0.5f;
    public float verticalOffset = 1f;

    private Vector3 originalPosition;

    
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate object
        transform.Rotate(new Vector3(0f, turnSpeed * Time.deltaTime, 0f));

        //bobbing effect
        Vector3 temp = originalPosition;
        temp.y += (amplitude * Mathf.Sin(Mathf.PI * Time.fixedTime)) + verticalOffset;

        transform.position = temp;
    }
}
