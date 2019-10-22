using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id : MonoBehaviour
{
    [SerializeField] int id = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.instance.ContainsId(id))
        {
            Destroy(this.gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetID()
    {
        return id;
    }

    public void SetID(int i)
    {
        id = i;
    }
}
