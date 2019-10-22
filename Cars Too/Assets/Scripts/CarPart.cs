using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPart : MonoBehaviour
{

    [SerializeField] int value;
    [SerializeField] int id=0;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.instance.ContainsIdCarPart(id))
        {
            Destroy(this.gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetValue()
    {
        return value;
    }

    public int GetID()
    {
        return id;
    }
}
