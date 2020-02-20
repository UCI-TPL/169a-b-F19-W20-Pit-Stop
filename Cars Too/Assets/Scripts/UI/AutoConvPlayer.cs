using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoConvPlayer : MonoBehaviour
{
    [SerializeField] private GameObject autodialogueobjects = null;
    [SerializeField] private GameObject dialogueobjs = null;
    [SerializeField] private GameObject objectiveobj;
    [SerializeField] private Chatlist cl;
    private ConfidantMenu cm;
    private DialogueManager dm;
    private float timer = 0.0f;
    private float timelimit = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        dm = GameObject.FindObjectOfType<DialogueManager>();
        autodialogueobjects = dm.autodialogueobjects;
        dialogueobjs = dm.canvasobjects;

        timer = 0.0f;
        timelimit = Random.Range(40.0f, 80.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!autodialogueobjects.activeSelf&&timer >= timelimit)
        {
            StartCoroutine(playConversation());
            
        }
        else
        {
            
            timer += Time.deltaTime;
        }
    }

    private IEnumerator playConversation()
    {
        objectiveobj.SetActive(false);
        if (!dialogueobjs.activeSelf && !autodialogueobjects.activeSelf)
        {
            dm.PushConversation(cl.chats[(int)Random.Range(0, cl.chats.Count)], false);
        }

        //Wait for dialogue to finish
        while (autodialogueobjects.activeSelf)
        {
            yield return null;
        }

        timer = 0.0f;
        timelimit = Random.Range(80.0f, 120.0f);
        objectiveobj.SetActive(true);


    }
}
