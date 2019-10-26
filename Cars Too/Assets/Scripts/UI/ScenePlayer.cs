using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenePlayer : MonoBehaviour
{
    public DialogueManager dm;
    [SerializeField] private Chatlist cl;
    public GameObject dialoguebox;
    private int index =-1;
    bool finished = true;

    // Start is called before the first frame update
    void Start()
    {
        dm = GameObject.FindObjectOfType<DialogueManager>();
        
    }

  

    private void PlayConvorChat(Chat c)
    {
        if (c.isConversation)
        {
            StartCoroutine(playConversation(c));
        }
        else
        {
            StartCoroutine(playChatConversation(c));
        }
    }

    private IEnumerator playConversation(Conversation c)
    {
        
        StartCoroutine(dm.playConversation(c.convo));

        //Wait for dialogue to finish
        while (dialoguebox.activeSelf)
        {
            yield return null;
        }

        Debug.Log("here1");
        finished = true;
    }

    private IEnumerator playChatConversation(Chat c)
    {
        //play the choice, and wait for a result
        StartCoroutine(dm.playChoice(c.convo, c.Choice1, c.Choice2));
        int choice = 0;
        while (choice == 0)
        {
            choice = dm.getChoice();
            yield return null;
        }

        //Play the conversation according to the choice, and show the player the result of their choice
        List<Dialogue> route = c.Route1;
        if (choice == 2)
        {
            //showResult(c.C2Reward >= c.C1Reward, c.C2Reward);
            route = c.Route2;
        }
        else
        {
            //showResult(c.C1Reward >= c.C2Reward, c.C1Reward);
        }

        StartCoroutine(dm.playConversation(route));

        //Wait for dialogue to finish
        while (dialoguebox.activeSelf)
        {
            yield return null;
        }

       
        finished = true;
    }

    private void Update()
    {
        if (finished)
        {
         
            index++;
            if (index < cl.chats.Count)
            {
                PlayConvorChat(cl.chats[index]);
            }
            finished = false;
        }
    }
}
