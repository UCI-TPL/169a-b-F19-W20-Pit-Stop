using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chief : MonoBehaviour
{
    [SerializeField] private List<Chat> randomchats = null; //holds all the dialogue for when the npc greets the player
    [SerializeField] public string Confidantname = ""; //determines where to reference confidant exp from in the list
    public bool close = false;  //checks to see if te player is close
    [SerializeField] private GameObject player;
    [SerializeField] private DialogueManager dm = null;
    [SerializeField] private Sprite consprite = null;
    [SerializeField] private GameObject dialoguebox;
    [SerializeField] private Chat introchat = null; //Custom intro chat for first meeting
    [SerializeField] private Conversation idlechats = null; //small lines said in the confidant menu
    [SerializeField] private GameObject talkui = null;
    [SerializeField] private int npcthemeindex = 0;
    [SerializeField] private int carPartsToNextPhase;
    bool running = false;
    bool moveToNextLevel = false;
    [SerializeField] private string destlevel = "Phase2CS";


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
        talkui = cm.npcchaticon;

        ResetcarPartsThreshold();
        moveToNextLevel = false;
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
        if (other.CompareTag("Player") && close != true)
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
        //cm.OpenMenu(this, consprite, Confidantname);
        talkui.SetActive(false);
        //close = false;
        dm.HaltDialogue();
        TurnOnSprite();
        if (!met)
        {
            PlayConvorChat(introchat);
            DataManager.instance.ConfidantMet(Confidantname);
        }
        else
        {
            playChat();
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
        //Pause PlayerMovement
        //PausePlayer();

        //Turn on Chief sprite
        TurnOnSprite();

        //If player has enough parts
        if(DataManager.instance.carParts >= carPartsToNextPhase)
        {
            //hardcoded positions: 0 = choice to move to next stage, 1 = not enough parts
            PlayConvorChat(randomchats[0]);
        }
        else
        {
            PlayConvorChat(randomchats[1]);
        }

        

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
        running = false;

        //Resume game
        closeMenu();
    }

    //Assume that this will only be used for moving to next level
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
            Debug.Log("No");
            //showResult(c.C2Reward >= c.C1Reward, c.C2Reward);
            route = c.Route2;
        }
        else
        {
            Debug.Log("Yes");
            if(c!=introchat)
                moveToNextLevel = true;
            //showResult(c.C1Reward >= c.C2Reward, c.C1Reward);
        }

        StartCoroutine(dm.playConversation(route));

        //Wait for dialogue to finish
        while (dialoguebox.activeSelf)
        {
            yield return null;
        }

        running = false;

        //Resume Game
        closeMenu();

        if (c == introchat)
        {
            openMenu();
        }
        if (moveToNextLevel)
        {
            Debug.Log("Move to level 2");
            MoveToLevel2();
        }

    }

    //Displays the result on affinity based on player choice
    public void showResult(bool goodChoice, int affinityrewarded)
    {
        //Display a happy face or an unhappy face here
        StartCoroutine(cm.AffinityIcon());
    }

    IEnumerator WaitforChattoEnd(Chat c)
    {

        while (running)
        {
            yield return null;
        }
        PlayConvorChat(c);

    }

    private void PausePlayer()
    {
        player.GetComponent<CarMovement>().Pause();
    }

    private void UnpausePlayer()
    {
        player.GetComponent<CarMovement>().Unpause();
    }

    public void ResetcarPartsThreshold()
    {
        carPartsToNextPhase = 3;
    }

    public void SetCarPartsThreshold(int n)
    {
        carPartsToNextPhase = n;
    }

    private void MoveToLevel2()
    {
        DataManager.instance.IncreasePhase();
        SceneManager.LoadScene(destlevel);
    }

    private void TurnOnSprite()
    {
        cm.confidant.sprite = consprite;
        cm.confidant.gameObject.SetActive(true);
    }

    private void TurnOffSprite()
    {
        cm.confidant.gameObject.SetActive(false);
    }


 }
