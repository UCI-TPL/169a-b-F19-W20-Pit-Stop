using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saws : MonoBehaviour
{
    //Entity reference to the entity the ability is attached to
    public Entity myself;
    public float abilitydamage = 2.5f; //The damage the ability does
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if the other hit is an entity and not myself
        Entity e = other.GetComponent<Entity>();
        if (e != null&&e!=myself)
        {
            //deal damage to it
            myself.AbilityDamage(abilitydamage,e);
            //and Knock it back
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-10, -10));
        }
    }
}
