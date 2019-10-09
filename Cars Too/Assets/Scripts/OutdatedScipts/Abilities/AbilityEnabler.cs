using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEnabler : MonoBehaviour
{
    //This is a base class for all abilities to inherit from and just serves as a baseline by which
    //ability data can be transmitted to the ability prefab

    public Ability abil=null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //sets the Ability equal to the given ability
    public void setAbility(Ability a)
    {
        abil = a;
    }
}
