using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] private Dictionary<int, List<Conversation>> IntroDialogue =null; //holds all the dialogue for when the npc greets the player
    [SerializeField] public int Confidantid = -1; //determines where to reference confidant exp from in the list
    public Sprite appearance = null; //determines where
    private bool close = false;  //checks to see if te player is close
    [SerializeField] private GameObject confidantmenu = null;
    
    void Start()
    {
        
    }

    //Checks to make sure values have been instantiated
    private void checkValues()
    {
        if (Confidantid == -1)
        {
            Debug.Log("Confidantid not instantiated");
        }

        if (appearance == null)
        {
            Debug.Log("appearance not instantiated");
        }

        if (IntroDialogue == null)
        {
            Debug.Log("IntroDialogue not instantiated");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (close && Input.GetKeyDown(KeyCode.E))
        {
            openMenu();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            close = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            close = false;
        }
    }

    private void openMenu()
    {
        confidantmenu.SetActive(true);
        //player.stopmoving
    }

}
