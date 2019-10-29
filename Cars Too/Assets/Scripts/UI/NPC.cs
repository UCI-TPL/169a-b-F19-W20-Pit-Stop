using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] private  List<Chat> randomchats =null; //holds all the dialogue for when the npc greets the player
    [SerializeField] public string Confidantname = ""; //determines where to reference confidant exp from in the list
    public bool close = false;  //checks to see if te player is close
    [SerializeField] private GameObject player;
    [SerializeField] private DialogueManager dm= null;
    [SerializeField] private Sprite consprite=null;
    [SerializeField] private GameObject dialoguebox;
    [SerializeField] private Chat introchat = null; //Custom intro chat for first meeting
    [SerializeField] private Conversation idlechats=null; //small lines said in the confidant menu
    [SerializeField] private GameObject talkui = null;
    [SerializeField] private List<int> giftvalues = new List<int>();
    [SerializeField] private Chatlist giftrecievedchats;
    
    private ConfidantMenu cm = null;


    private bool met
    {
        get
        {
            return DataManager.instance.GetConfidantData(Confidantname).met;
        }
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cm = GameObject.FindObjectOfType<ConfidantMenu>();
    }

    //Checks to make sure values have been instantiated
    private void checkValues()
    {
        if (Confidantname == "")
        {
            Debug.Log("Confidantname not instantiated");
        }

        if (randomchats == null)
        {
            Debug.Log("randomchats not instantiated");
        }

        if (consprite == null)
        {
            Debug.Log("no confidant sprite");
        }


        if (dm == null)
        {
            dm = GameObject.FindObjectOfType<DialogueManager>();
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
            talkui.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            close = false;
            talkui.SetActive(false);
        }
    }

    public void openMenu()
    {
        cm.OpenMenu(this,consprite, Confidantname);
        talkui.SetActive(false);
        close = false;
        dm.setNPC(this,consprite);
        if (!met)
        {
            //StartCoroutine(playChatConversation(introchat));
            PlayConvorChat(introchat);
            DataManager.instance.ConfidantMet(Confidantname);
        }
        else
        {
            dm.DisplayLine(idlechats.convo[Random.Range(0, idlechats.convo.Count)]);
        }

        //player.stopmoving
        player.GetComponent<CarMovement>().Pause();
    }

    public void closeMenu()
    {
        cm.CloseMenu();
        //confidantimage.gameObject.SetActive(false);
        close = true;
        talkui.SetActive(true);
        dm.CloseLine();
        //player can move again
        player.GetComponent<CarMovement>().Unpause();
    }


    public void playChat()
    {
        PlayConvorChat(randomchats[Random.Range(0, randomchats.Count)]);
      
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
        cm.CloseMenu(false);
        StartCoroutine(dm.playConversation(c.convo));

        //Wait for dialogue to finish
        while (dialoguebox.activeSelf)
        {
            yield return null;
        }

        //restart confidant menu
        openMenu();
    }

    private IEnumerator playChatConversation(Chat c)
    {
        cm.CloseMenu(false);
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
            showResult(c.C2Reward >= c.C1Reward, c.C2Reward);
            route = c.Route2;
        }
        else
        {
            showResult(c.C1Reward >= c.C2Reward, c.C1Reward);
        }

        StartCoroutine(dm.playConversation(route));

        //Wait for dialogue to finish
        while (dialoguebox.activeSelf)
        {
            yield return null;
        }

        //restart confidant menu
        openMenu();
    }

    //Displays the result on affinity based on player choice
    public void showResult(bool goodChoice, int affinityrewarded)
    {
        //Display a happy face or an unhappy face here
        addAffinity(affinityrewarded);
    }

    //Adds Affinity to Confidant TBC
    public void addAffinity(int amount)
    {
        if(DataManager.instance.AddConfidantAffinity(Confidantname, amount))
        {
            Debug.Log("LEVELED UP");
        }
        
    }

    public void RecieveGift(PresentType present)
    {
        
        int affinitygained = giftvalues[(int)present];
        showResult(affinitygained >= 25, affinitygained);
        PlayConvorChat(giftrecievedchats.chats[(int)present]);

    }

    
}
