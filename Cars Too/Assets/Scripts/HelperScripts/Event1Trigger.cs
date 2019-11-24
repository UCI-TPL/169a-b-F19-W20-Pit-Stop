using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Event1Trigger : Id
{

    [SerializeField] private string destscene = "Event1"; //Scene to transition to
                                                          //Need to save player location, and potentially account for better settings for other events
                                                          //Once other event triggers are known.


    void Update()
    {
        //Might need to have an additional check to see if player is grounded Otherwise transitioning back will cause the player to fall
        if (DataManager.instance.carParts >= 2)
        {
            DataManager.instance.AddID(GetID());
            SceneManager.LoadScene(destscene);
        }
    }
}
