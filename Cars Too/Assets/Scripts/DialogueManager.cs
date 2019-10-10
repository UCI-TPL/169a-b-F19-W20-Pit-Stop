using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{   
    //The Text locations for the dialouge, and speaker name
    [SerializeField] private TextMeshProUGUI dialgouebox;
    [SerializeField] private TextMeshProUGUI speakerbox;
    [SerializeField] private GameObject canvasobjects; //visible canvas objects associated with dialogue to be turned on or off
    [SerializeField] private int index = 0; //holds current place in dialogue
    [SerializeField] private int speed = 1; //controls speed at which text appears
    [SerializeField] private bool instant = false; //detects whether the text should be displayed instantly
    [SerializeField] private bool running = false;
    [SerializeField] private bool finished = false;
    [SerializeField] private Conversation test;

    //All fields are currently serialized for testing purposes

    void Start()
    {
        canvasobjects.SetActive(false);
        StartCoroutine(playConversation(test.converttoDialogue()));
    }

    // Update is called once per frame
    void Update()
    {
        //Detect Left Clicks and instant display text if it is not all displayed
        if (Input.GetKeyDown(KeyCode.Mouse0)&&running)
        {
            instant = true;
        }
    }

    public IEnumerator playConversation(List<Dialogue> d)
    {
        canvasobjects.SetActive(true);
        for(int i=0; i < d.Count; i++)
        {
            StartCoroutine(playDialogue(d[i]));
            while (running)
            {
                
                yield return null;

            }

            yield return null;

            while (!finished)
            {
                yield return null;

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    finished = true;
                }
                
            }

            finished = false;
        }

        canvasobjects.SetActive(false);
    }

    public IEnumerator playDialogue(Dialogue d)
    {
        //Reset defaults and set speaker
        running = true;
        speakerbox.text = d.speaker;
        dialgouebox.text = "";
        index = 0;

        while(index<d.text.Length)
        {
            //if the player has clicked display all text
            if (instant)
            {
                running = false;
                dialgouebox.text = d.text;
                instant = false;
                index = d.text.Length;
                break;
                
            }
            else
            {
                //Otherwise display a number of chars determined by speed
                for(int i =index; i< index+speed; i++)
                {
                    if (i >= d.text.Length)
                    {
                        running = false;
                        break;
                    }

                    dialgouebox.text += d.text[i];
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
}
