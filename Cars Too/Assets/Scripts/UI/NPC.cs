using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{

    [SerializeField] public string Confidantname = ""; //determines where to reference confidant exp from in the list
    public bool close = false;  //checks to see if te player is close
    [SerializeField] private GameObject player; 
    [SerializeField] private DialogueManager dm= null;
    [SerializeField] private Sprite consprite=null; //portrait
    [SerializeField] public Sprite npcbg = null; //bg to be displayed in confidant menu
    [SerializeField] private GameObject dialoguebox; //dialm object
    [SerializeField] private GameObject talkui = null;//e to interact ui
    [SerializeField] private List<int> giftvalues = new List<int>(); //affinity values for each gift
    private ConfidantMenu cm = null; // confidantmenu reference
    bool running = false; //indicates when a chat is being processed 

    //index for the npc's theme within the audiomanager
    [SerializeField] private int npcthemeindex=0;


    //Different Chat and Conversation SO 
    [SerializeField] private List<Chatlist> randomchats = null; //holds chat dialogue that is played when the player chooses to chat
    [SerializeField] private Chatlist LevelupChats; //chats that are played on confidant lvlup
    [SerializeField] private Chatlist giftrecievedchats; //chats that are played when a gift is received
    [SerializeField] private Chat introchat = null; //Custom intro chat for first meeting
    [SerializeField] private Conversation idlechats = null; //small lines said in the confidant menu
    [SerializeField] private Chat nomorechats = null; //default line for when there are no more chats in the current phase
    [SerializeField] private Chatlist date1 = null; //chat to be played when date is clicked


    



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
        dm = GameObject.FindObjectOfType<DialogueManager>();
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
        //if we exceed the current amount of chats, play the default chat
        if (nomorechatsaval())
        {
            PlayConvorChat(nomorechats);
        }
        else
        {
            //otherwise we play the chat and increment the index
            PlayConvorChat(randomchats[DataManager.instance.phase].chats[DataManager.instance.GetConfidantData(Confidantname).getchatindex()]);
            DataManager.instance.GetConfidantData(Confidantname).incrementchatindex();
        }
      
    }

    public bool nomorechatsaval() {
        return DataManager.instance.GetConfidantData(Confidantname).getchatindex() >= randomchats[DataManager.instance.phase].chats.Count;
    }

    private void PlayConvorChat(Chat c)
    {
        running = true;
        DataManager.instance.am.PlayandTrackBGM(npcthemeindex);
        
        if (c.isConversation)
        {
            StartCoroutine(playConversation(c));
        }
        else
        {
            StartCoroutine(playChatConversation(c));
        }
    }

    private IEnumerator playConversation(Chat c)
    {
        cm.CloseMenu(false);
        StartCoroutine(dm.playConversation(c.convo));

        //Wait for dialogue to finish
        while (dialoguebox.activeSelf)
        {
            yield return null;
        }
        if (c.C1Reward > 0)
        {
            showResult(true, c.C1Reward);
        }

        //Give any additional rewards
        Rewards(c);
        //restart confidant menu
        openMenu();
        running = false;
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

        //Give any additional rewards
        Rewards(c);
        //restart confidant menu
        openMenu();
        running = false;

    }

    //Displays the result on affinity based on player choice
    public void showResult(bool goodChoice, int affinityrewarded)
    {
        //Display a happy face or an unhappy face here
        StartCoroutine(cm.AffinityIcon());
        addAffinity(affinityrewarded);
    }

    //Adds Affinity to Confidant TBC
    public void addAffinity(int amount)
    {
        if(DataManager.instance.AddConfidantAffinity(Confidantname, amount))
        {
            if (LevelupChats.chats[DataManager.instance.GetConfidantLevel(Confidantname)-1]!=null)
            {
                StartCoroutine(WaitforChattoEnd(LevelupChats.chats[DataManager.instance.GetConfidantLevel(Confidantname) - 1]));
            }
            
            Debug.Log("LEVELED UP");
        }
        
    }

    IEnumerator WaitforChattoEnd(Chat c)
    {
        
        while (running) {
            yield return null;
        }
        PlayConvorChat(c);
        
    }

    public void RecieveGift(PresentType present)
    {
        
        int affinitygained = giftvalues[(int)present];
        PlayConvorChat(giftrecievedchats.chats[(int)present]);
        showResult(affinitygained >= 25, affinitygained);

    }

    private void Rewards(Chat c)
    {
        //give abilitys or carparts based on chat
        if (c.abilityactivation)
        {
            if (Confidantname.Equals("Piper"))
            {
                DataManager.instance.canBoost = true;
            }
            else if (Confidantname.Equals("Dex"))
            {
                DataManager.instance.affinityBoost = true;
            }
            else if (Confidantname.Equals("Loco"))
            {
                DataManager.instance.canHack = true;
            }
            else if (Confidantname.Equals("Springtrap"))
            {
                DataManager.instance.canDestroy = true;
            }
            else if (Confidantname.Equals("Mustang"))
            {
                DataManager.instance.canThrow = true;
            }
        }
        else if (c.carpartsgained>0)
        {
            DataManager.instance.AddCarParts(c.carpartsgained);
        }

    }

    public void PlayDate()
    {
        StartCoroutine(PlayDaterun(date1));
    }

    private IEnumerator PlayDaterun(Chatlist date)
    {
        foreach (Chat c in date.chats)
        {
            while (running)
            {
                yield return null;
            }
            PlayConvorChat(c);
        }

    }

}
