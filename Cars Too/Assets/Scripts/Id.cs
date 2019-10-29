using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id : MonoBehaviour
{
    //IF YOU WANT TO HAVE IDS ON A CLASS JUST INHERIT FROM THIS CLASS
    //If an ID is addded to the datamanagers list of ids on reloading the scene it will do something
    //At base the gameobject is just deleted if the id is contained
    //Any Class that inherits from this one will get the ID editor as well
    [SerializeField] int id = 0;
    public virtual void Start()
    {
        //If the id is contained do something
        if (DataManager.instance.ContainsId(id))
        {
            IDContained();
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Returns ID
    public int GetID()
    {
        return id;
    }
    
    //Overrite this function if you want something else to happen instead of deletion!
    public virtual void IDContained()
    {
        
        Destroy(this.gameObject);
    }

    //Sets ID only to be used by the editor
    public void SetID(int i)
    {
        id = i;
    }
}
