using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueLog : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> dialogueboxes;
    [SerializeField] List<TextMeshProUGUI> speakernames;
    [SerializeField] GameObject dlog;
    private List<Dialogue> dialogues;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        dialogues = new List<Dialogue>();
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

    

}
