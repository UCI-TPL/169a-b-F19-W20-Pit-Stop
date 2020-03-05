using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelForceUnlock : MonoBehaviour
{
    // Used to force unlock abilities in the endlevel
    void Start()
    {
        DataManager.instance.canDestroy = true;
        DataManager.instance.canThrow = true;
        DataManager.instance.canHack = true;
        DataManager.instance.canBoost = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
