using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //The Text locations for the dialouge, and speaker name
    [SerializeField] private TextMeshProUGUI dialgouebox;
    [SerializeField] private TextMeshProUGUI speakerbox;
    [SerializeField] private GameObject canvasobjects; //visible canvas objects associated with dialogue to be turned on or off
    [SerializeField] private float index = 0; //holds current place in dialogue
    [SerializeField] private int speed = 1; //controls speed at which text appears
    [SerializeField] private bool instant = false; //detects whether the text should be displayed instantly
    [SerializeField] private bool running = false; //Detects whether the dialogue coroutine is running
    [SerializeField] private bool finished = false; //Detects if the player is ready to proceed to the next line
    [SerializeField] private int choice = 0; //Used to return the result of a choice
    [SerializeField] private GameObject Choicecanvas;
    [SerializeField] private TextMeshProUGUI Choicetext1;
    [SerializeField] private TextMeshProUGUI Choicetext2;
    [SerializeField] public NPC currentnpc;
    [SerializeField] private AudioClip ac;
    [SerializeField] private ScenePortraits sp = null;
    [SerializeField] private bool skip = false;
    [SerializeField] private GameObject skipbutton = null;
    [SerializeField] private float timer = 3.5f; //wait time when autoplaying dialogue
    
    [SerializeField] public GameObject autodialogueobjects =null; //gameobject to be turned on when playing autodialogue
    [SerializeField] private TextMeshProUGUI autodialoguebox = null; //textbox for autodialogue
    [SerializeField] private AutoDialoguePortrait adp = null; //character portrait for autodialogue
    [SerializeField] private GameObject blinker = null; //blinker obj to display when done reading text

    [SerializeField] private List<Color> namecolors;
    [SerializeField] private Image dboxoutline;
    [SerializeField] private Image sboxoutline;


    //All fields are currently serialized for testing purposes

    void Start()
    {
        skip = false;
        canvasobjects.SetActive(false);
        Choicecanvas.SetActive(false);
        //StartCoroutine(playChoice(test.convo,test.Choice1,test.Choice2));
    }

    // Update is called once per frame
    void Update()
    {
        //Detect Left Clicks and instant display text if it is not all displayed
        if (GetInput() && running)
        {
            instant = true;
        }
    }
    public void setNPC(NPC npc, Sprite s)
    {
        currentnpc = npc;
        //currentspeaker.sprite = s;
    }

    public void DisplayLine(Dialogue d)
    {
        canvasobjects.SetActive(true);
        dialgouebox.text = d.text;
        speakerbox.text = d.speaker;
        skipbutton.SetActive(false);
    }

    public void CloseLine()
    {
        canvasobjects.SetActive(false);
    }

    public IEnumerator playChoice(List<Dialogue> d, string choice1, string choice2)
    {
        StartCoroutine(playConversation(d));

        //used to check when playconversation is done, and the canvas objs are turned off
        while (canvasobjects.activeSelf)
        {
            yield return null;
        }
        canvasobjects.SetActive(true);
        Choicecanvas.SetActive(true);
        skipbutton.SetActive(false);
        Choicetext1.text = choice1;
        Choicetext2.text = choice2;

        while (Choicecanvas.activeSelf)
        {

            yield return null;
        }
        canvasobjects.SetActive(false);
    }

    public bool GetInput()
    {
        return Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return);
    }

    //overrides other dialogues
    public void PushConversation(Chat c,bool normalchat=true)
    {
            StopAllCoroutines();
            autodialogueobjects.SetActive(false);
            canvasobjects.SetActive(false);

        //if the chat is not normal, it is an auto chat
        if (!normalchat)
        {
            StartCoroutine(playAutoConversation(c.convo));
        }
        else
        {

            StartCoroutine(playConversation(c.convo,false));
        }
            
            
        
    }

    public IEnumerator playConversation(List<Dialogue> d, bool canskip = true)
    {
        //Turn on the Dialogue canvas objects
        canvasobjects.SetActive(true);
        DetermineNameColor(d[0].speaker);
        skipbutton.SetActive(canskip);

        //For each line of dialogue play it separately
        for (int i = 0; i < d.Count; i++)
        {
           
            StartCoroutine(playDialogue(d[i]));

            //Wait here for dialogue to play
            while (running)
            {

                yield return null;

            }

            if (skip)
            {
                
                speakerbox.text = d[d.Count-1].speaker;
                dialgouebox.text = d[d.Count-1].text;
                skip = false;
                break;
            }
            blinker.SetActive(true);
            //Wait a frame to allow for a click to expedite dialogue to not be counted again when proceeding to the next line.
            yield return null;

            

            //Wait here with dialogue displayed until player clicks
            while (!finished)
            {
                yield return null;

                if (GetInput())
                {
                    blinker.SetActive(false);
                    finished = true;
                    DataManager.instance.am.PlaySound(ac);
                }

            }

            
            //reset variable
            finished = false;
        }

        //Turn canvas off once all dialogue has been played
        canvasobjects.SetActive(false);
    }

    public IEnumerator playDialogue(Dialogue d)
    {
        //Reset defaults and set speaker
        running = true;
        DetermineNameColor(d.speaker);
        speakerbox.text = d.speaker.Replace("Lightning",DataManager.instance.GetName());
        dialgouebox.text = "";
        index = 0;
        string dialoguetext = d.text.Replace("Lightning", DataManager.instance.GetName());
        
        if (sp != null)
        {
            sp.UpdatePortraits(d.speaker);
        }
        while (index < dialoguetext.Length)
        {
            //if the player has clicked display all text
            if (instant)
            {
                running = false;
                dialgouebox.text = dialoguetext;
                instant = false;
                index = dialoguetext.Length;
                //DataManager.instance.am.PlaySound(ac);
                break;

            }
            else if (skip)
            {
                break;
            }
            else
            {
                //Otherwise display a number of chars determined by speed
                for (int i = Mathf.CeilToInt(index); i < index + DataManager.instance.textspeed; i++)
                {
                    if (i >= dialoguetext.Length)
                    {
                        running = false;
                        break;
                    }

                    dialgouebox.text += dialoguetext[i];
                }
                index += DataManager.instance.textspeed;
            }

            //Yield until next frame and then repeat
            yield return null;
        }

        running = false;
    }

    public void setTextSpeed(int newspeed)
    {
        speed = newspeed;
    }

    public int getChoice()
    {
        if (choice == 0)
        {
            return choice;
        }
        else
        {

            int temp = choice;
            choice = 0;
            return temp;
        }
    }

    //Used by choice buttons to set correct choice
    public void setChoice(int i)
    {
        choice = i;
        Choicecanvas.SetActive(false);
    }

    public void chat()
    {
        currentnpc.playChat();
    }

    public void skipped()
    {
        skip = true;
    }


    public IEnumerator playAutoConversation(List<Dialogue> d)
    {
        //Turn on the Dialogue canvas objects
        autodialogueobjects.SetActive(true);
        //skipbutton.SetActive(false);

        //For each line of dialogue play it separately
        for (int i = 0; i < d.Count; i++)
        {
            StartCoroutine(playAutoDialogue(d[i]));

            //Wait here for dialogue to play
            while (running)
            {

                yield return null;

            }


            //Wait a frame to allow for a click to expedite dialogue to not be counted again when proceeding to the next line.
            yield return null;


            float temp = 0.0f;
            //Wait here with dialogue displayed until player clicks
            while (!finished)
            {

                temp += Time.deltaTime;
                yield return null;
                
                if (temp>=timer)
                {

                    finished = true;
                    DataManager.instance.am.PlaySound(ac);
                    temp = 0.0f;
                }

            }

            //reset variable
            finished = false;
        }

        //Turn canvas off once all dialogue has been played
        autodialogueobjects.SetActive(false);
    }

    //plays dialogue that automatically progresses
    public IEnumerator playAutoDialogue(Dialogue d)
    {
        //Reset defaults and set speaker
        running = true;
        adp.ShowPortrait(d.speaker);
        //speakerbox.text = d.speaker.Replace("Lightning", DataManager.instance.GetName());
        autodialoguebox.text = "";
        index = 0;
        string dialoguetext = d.text.Replace("Lightning", DataManager.instance.GetName());

      

        while (index < dialoguetext.Length)
        {
            //if the player has clicked display all text

                //Otherwise display a number of chars determined by speed
                for (int i = Mathf.CeilToInt(index); i < index + DataManager.instance.textspeed; i++)
                {
                    if (i >= dialoguetext.Length)
                    {
                        
                        running = false;
                        break;
                    }

                    autodialoguebox.text += dialoguetext[i];
                }
                index += DataManager.instance.textspeed;


            //Yield until next frame and then repeat
            yield return null;
        }


        running = false;
    }

    public void HaltDialogue()
    {
        StopAllCoroutines();
        canvasobjects.SetActive(false);
    }

    private void DetermineNameColor(string n)
    {
        if (n.Equals("Chief"))
        {
            sboxoutline.color = namecolors[0];
            dboxoutline.color = namecolors[0];
            //speakerbox.color = namecolors[0];
        }
        else if (n.Equals("Dex"))
        {
            sboxoutline.color = namecolors[1];
            dboxoutline.color = namecolors[1];
            //speakerbox.color = namecolors[1];
        }
        else if (n.Equals("Lightning"))
        {
            sboxoutline.color = namecolors[2];
            dboxoutline.color = namecolors[2];
            //speakerbox.color = namecolors[2];
        }
        else if (n.Equals("Mustang"))
        {
            sboxoutline.color = namecolors[3];
            dboxoutline.color = namecolors[3];
            //speakerbox.color = namecolors[3];
        }
        else if (n.Equals("Piper"))
        {
            sboxoutline.color = namecolors[4];
            dboxoutline.color = namecolors[4];
            //speakerbox.color = namecolors[4];
        }
        else if (n.Equals("Springtrap"))
        {
            sboxoutline.color = namecolors[5];
            dboxoutline.color = namecolors[5];
            //speakerbox.color = namecolors[5];
        }
        else
        {
            sboxoutline.color = namecolors[2];
            dboxoutline.color = namecolors[2];
            Debug.Log("NAME COLOR NOT FOUND");
        }
    }
}
