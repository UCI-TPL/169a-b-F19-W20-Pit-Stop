using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] private  List<Chat> randomchats =null; //holds all the dialogue for when the npc greets the player
    [SerializeField] public int Confidantid = -1; //determines where to reference confidant exp from in the list
    public Sprite appearance = null; //determines where
    public bool close = false;  //checks to see if te player is close
    [SerializeField] private GameObject confidantmenu = null;
    [SerializeField] private DialogueManager dm= null;
    [SerializeField] private Image confidantimage = null;
    [SerializeField] private Sprite consprite=null;
    [SerializeField] private GameObject dialoguebox;


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

        if (randomchats == null)
        {
            Debug.Log("randomchats not instantiated");
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

    public void openMenu()
    {
        confidantmenu.SetActive(true);
        confidantimage.gameObject.SetActive(true);
        confidantimage.sprite = consprite;
        close = false;
        //player.stopmoving
    }

    public void closeMenu()
    {
        confidantmenu.SetActive(false);
        confidantimage.gameObject.SetActive(false);
        confidantimage.sprite = consprite;
        close = true;
        //player can move again
    }


    public void playChat()
    {

        StartCoroutine(waitForChoice());
    }

    private IEnumerator waitForChoice()
    {
        //hide menu
        confidantmenu.SetActive(false);
        
        //Choose a chat at random
        int randint = Random.Range(0, randomchats.Count - 1);

        //play the choice, and wait for a result
        StartCoroutine(dm.playChoice(randomchats[randint].convo, randomchats[randint].Choice1, randomchats[randint].Choice2));
        int choice = 0;
        while (choice == 0)
        {
            choice = dm.getChoice();
            yield return null;
        }
        
        //Play the conversation according to the choice, and show the player the result of their choice
        List<Dialogue> route = randomchats[randint].Route1;
        if (choice == 2)
        {
            showResult(randomchats[randint].C2Reward >= randomchats[randint].C1Reward, randomchats[randint].C2Reward);
            route= randomchats[randint].Route2;
        }
        else
        {
            showResult(randomchats[randint].C1Reward >= randomchats[randint].C2Reward, randomchats[randint].C1Reward);
        }
        
        StartCoroutine(dm.playConversation(route));

        //Wait for dialogue to finish
        while (dialoguebox.activeSelf)
        {
            yield return null;
        }

        //restart confidant menu
        confidantmenu.SetActive(true);
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

    }
}
