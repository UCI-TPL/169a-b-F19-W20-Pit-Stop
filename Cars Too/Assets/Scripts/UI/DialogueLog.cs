using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueLog : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> dialogueboxes;
    [SerializeField] List<TextMeshProUGUI> speakernames;
    [SerializeField] List<Image> chibiholders;
    //number order chief, dex, lightning, mustang, piper, springtrap
    [SerializeField] List<Sprite> chibisprites;
    [SerializeField] GameObject dlog;
    [SerializeField] List<Color> namecolors;
    private List<Dialogue> dialogues;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        dialogues = new List<Dialogue>();
        namecolors = GameObject.FindObjectOfType<DialogueManager>().namecolors;
    }

    // Update is called once per frame
    void Update()
    {
        if(dlog.activeSelf&&(Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow)))
        {
            MoveUp();
        }
        else if (dlog.activeSelf&&(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            Debug.Log(dialogueboxes[0].gameObject.activeSelf);
            MoveDown();
        }

    }

    public void UpdateLog(Dialogue d)
    {
        dialogues.Add(d);
    }

    public void DisplayLog(bool first = false)
    {
        for (int i = 0; i < dialogueboxes.Count; i++)
        {
            if (first)
            {
                index = dialogues.Count - 1;
            }

            if (dialogues.Count > i)
            {
                dialogueboxes[i].text = dialogues[index - i].text;
                speakernames[i].text = dialogues[index - i].speaker;
                
            }
            else
            {
                dialogueboxes[i].text = "";
                speakernames[i].text = "";
            }
            UpdateChibi(chibiholders[i], speakernames[i].text, speakernames[i]);
        }
        
    }

    public void MoveDown()
    {
        if (index < dialogues.Count-1)
        {
            index++;
            DisplayLog();
        }
    }

    public void MoveUp()
    {
        if (index > 4)
        {
            index--;
            DisplayLog();
        }
    }

    public void PlayVLine(int dnumber)
    {
        if (index-dnumber>=0 && dialogues[index - dnumber].voiceline != null)
        {
            DataManager.instance.am.PlayVL(dialogues[index - dnumber].voiceline);
        }
    }

    private void UpdateChibi(Image chibi, string speaker, TextMeshProUGUI speakertext)
    {
        if (speaker.Equals("Chief"))
        {
            speakertext.color = namecolors[0];
            chibi.sprite = chibisprites[0];
        }
        else if (speaker.Equals("Dex"))
        {
            speakertext.color = namecolors[1];
            chibi.sprite = chibisprites[1];
        }
        else if (speaker.Equals("Lightning"))
        {
            speakertext.color = namecolors[6];
            chibi.sprite = chibisprites[2];
        }
        else if (speaker.Equals("Mustang"))
        {
            speakertext.color = namecolors[3];
            chibi.sprite = chibisprites[3];
        }
        else if (speaker.Equals("Piper"))
        {
            speakertext.color = namecolors[4];
            chibi.sprite = chibisprites[4];
        }
        else if (speaker.Equals("Springtrap"))
        {
            speakertext.color = namecolors[5];
            chibi.sprite = chibisprites[5];
        }
        else if (speaker.Equals("Gak"))
        {
            speakertext.color = namecolors[2];
            chibi.sprite = chibisprites[6];
        }
        else
        {
            speakertext.color = namecolors[6];
            chibi.sprite = chibisprites[2];
            Debug.Log("NAME COLOR NOT FOUND");
        }
    }

    

}
