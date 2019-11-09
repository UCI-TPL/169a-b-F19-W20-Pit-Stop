using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    Rigidbody rb;
    public float m = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.mass = 10000;
    }

    // Update is called once per frame
    void Update()
    {
        if (DataManager.instance.canDestroy)
        {
            rb.mass = m;
            Destroy(this);
        }
    }
}
