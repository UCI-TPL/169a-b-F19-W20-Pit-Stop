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
    [SerializeField] private int index = 0; //holds current place in dialogue
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
    [SerializeField] private float time = .6f;



    [SerializeField] private Chat test; //Only used for testing purposes

    //All fields are currently serialized for testing purposes

    void Start()
    {
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

        Choicecanvas.SetActive(true);
        Choicetext1.text = choice1;
        Choicetext2.text = choice2;

        while (Choicecanvas.activeSelf)
        {

            yield return null;
        }
    }

    public bool GetInput()
    {
        return Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return);
    }

    //overrides other dialogues
    public void PushConversation(Chat c)
    {
            StopAllCoroutines();
            StartCoroutine(playAutoConversation(c.convo));
        
    }

    public IEnumerator playConversation(List<Dialogue> d)
    {
        //Turn on the Dialogue canvas objects
        canvasobjects.SetActive(true);
        skipbutton.SetActive(true);

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
                skip = false;
                break;
            }
            //Wait a frame to allow for a click to expedite dialogue to not be counted again when proceeding to the next line.
            yield return null;

            

            //Wait here with dialogue displayed until player clicks
            while (!finished)
            {
                yield return null;

                if (GetInput())
                {
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
        speakerbox.text = d.speaker.Replace("Lightning",DataManager.instance.GetName());
        dialgouebox.text = "";
        index = 0;
        string dialoguetext = d.text.Replace("Lightning", DataManager.instance.GetName());
        if (sp != null)
        {
            sp.UpdatePortraits(d.speaker);
        }
        while (index < d.text.Length)
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
                for (int i = index; i < index + speed; i++)
                {
                    if (i >= dialoguetext.Length)
                    {
                        running = false;
                        break;
                    }

                    dialgouebox.text += dialoguetext[i];
                }
                index += speed;
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
        canvasobjects.SetActive(true);
        skipbutton.SetActive(false);

        //For each line of dialogue play it separately
        for (int i = 0; i < d.Count; i++)
        {
            StartCoroutine(playDialogue(d[i]));

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
                
                if (temp>=time)
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
        canvasobjects.SetActive(false);
    }

    //plays dialogue that automatically progresses
    public IEnumerator playAutoDialogue(Dialogue d)
    {
        //Reset defaults and set speaker
        running = true;
        speakerbox.text = d.speaker.Replace("Lightning", DataManager.instance.GetName());
        dialgouebox.text = "";
        index = 0;
        string dialoguetext = d.text.Replace("Lightning", DataManager.instance.GetName());
        skipbutton.SetActive(false);
      

        while (index < d.text.Length)
        {
            //if the player has clicked display all text

                //Otherwise display a number of chars determined by speed
                for (int i = index; i < index + speed; i++)
                {
                    if (i >= dialoguetext.Length)
                    {
                        running = false;
                        break;
                    }

                    dialgouebox.text += dialoguetext[i];
                }
                index += speed;

            //Yield until next frame and then repeat
            yield return null;
        }

        running = false;
    }
}
