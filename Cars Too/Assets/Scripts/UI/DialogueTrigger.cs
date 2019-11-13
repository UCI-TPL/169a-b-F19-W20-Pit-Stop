using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Chat chat = null;//Chat to be played
    private DialogueManager dm;
    public GameObject dialoguebox;
    private bool hastriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        dm = GameObject.FindObjectOfType<DialogueManager>();
    }

    private IEnumerator playConversation(Chat c)
    {

        dm.PushConversation(c);

        //Wait for dialogue to finish
        while (dialoguebox.activeSelf)
        {
            yield return null;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hastriggered&&other.CompareTag("Player"))
        {
            StartCoroutine(playConversation(chat));
            hastriggered = true;
        }
    }
}
