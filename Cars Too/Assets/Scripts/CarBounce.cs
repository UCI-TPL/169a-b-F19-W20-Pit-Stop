using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBounce : CollectibleMovement
{
    // Start is called before the first frame update
    public float bobspeed = 4;

    // private void Start() {
    //     base.Start();
    // }


    private void Update() {
        Vector3 temp = originalPosition;
        temp.y += (amplitude * Mathf.Sin(Mathf.PI * Time.fixedTime*bobspeed));

        transform.position = temp;        
    }
}
