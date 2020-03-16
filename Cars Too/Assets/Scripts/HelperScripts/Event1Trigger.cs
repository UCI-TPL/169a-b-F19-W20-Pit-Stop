using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Event1Trigger : Id
{

    [SerializeField] public string destscene = "Event1"; //Scene to transition to
    [SerializeField] public int partsneeded = 2;          //Need to save player location, and potentially account for better settings for other events
                                                          //Once other event triggers are known.
    private LoadingScreen ls;

    private void Start()
    {
        ls = GameObject.FindObjectOfType<LoadingScreen>();
    }

    void Update()
    {
        //Might need to have an additional check to see if player is grounded Otherwise transitioning back will cause the player to fall
        if (DataManager.instance.carParts >= partsneeded)
        {
            DataManager.instance.AddID(GetID());
            ls.StartLoad(destscene);
            //SceneManager.LoadScene(destscene);
        }
    }
}
