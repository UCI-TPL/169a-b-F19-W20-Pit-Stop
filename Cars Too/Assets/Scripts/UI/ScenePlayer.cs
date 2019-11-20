using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenePlayer : MonoBehaviour
{
    public DialogueManager dm;
    [SerializeField] private Chatlist cl;
    public GameObject dialoguebox;
    private int index =-1;
    bool finished = true;
    [SerializeField] private Fadein fi;
    //Scene to go to after finishing
    [SerializeField] private string destscene="LVL1";

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
        //Check if dialogue is finished
        //if it is and there is more dialogue play it
        //Otherwise go to the dest scene
        if (finished)
        {
         
            index++;
            if (index < cl.chats.Count)
            {
                PlayConvorChat(cl.chats[index]);
            }
            else if(fi!=null&&!fi.gameObject.activeSelf)
            {
                fi.gameObject.SetActive(true);
                
            }
            else if(fi==null||fi.donefadeing)
            {
                
                SceneManager.LoadScene(destscene);
            }
            finished = false;
        }
        else if (fi != null && fi.donefadeing)
        {
            
            SceneManager.LoadScene(destscene);
        }
    }
}
