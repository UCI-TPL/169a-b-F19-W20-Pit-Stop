using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examine : MonoBehaviour
{
    public Chat chat = null;//Chat to be played
    public Chat piperexamine = null; //chat to be played after advanced examine is unlocked
    private DialogueManager dm;
    public GameObject dialoguebox;
    private bool close = false;
    [SerializeField] private GameObject uipopup;

    [SerializeField] private CarMovement cm;
    // Start is called before the first frame update
    void Start()
    {
        cm = GameObject.FindObjectOfType<CarMovement>();
        dm = GameObject.FindObjectOfType<DialogueManager>();
        uipopup.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && close)
        {
                StartCoroutine(playConversation(chat));
        }
    }

    private IEnumerator playConversation(Chat c)
    {
        if (!DataManager.instance.piperExamine)
        {
            dm.PushConversation(chat);
        }
        else
        {
            dm.PushConversation(piperexamine);

        }
       
        uipopup.SetActive(false);
        cm.Pause();
        

        //Wait for dialogue to finish
        while (dialoguebox.activeSelf)
        {
            yield return null;
        }
        uipopup.SetActive(true);
        cm.Unpause();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            close = true;
            uipopup.SetActive(true);
        }
    }

    //triggers on stay to allow triggers to be placed on the player's start point
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            close = true;
            uipopup.SetActive(true);
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            close = false;
            uipopup.SetActive(false);
        }
    }
}
