﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Chat chat = null;//Chat to be played
    private DialogueManager dm;
    public GameObject dialoguebox;
    private bool hastriggered = false; //a safety check to prvent excess dialogue triggers
    [SerializeField] private bool triggerablebyhat = false; //whether the player's hat can trigger the dialogue
    [SerializeField] private Fadein fi = null; // fadein to occur after dialogue trigger
    [SerializeField] private CarMovement cm;
    [SerializeField] private bool important = true; //signals whether the rest of the dialogue should stop to play this, and whether to pause the player or not
    //if null just does nothing.
    // Start is called before the first frame update
    void Start()
    {
        cm = GameObject.FindObjectOfType<CarMovement>();
        dm = GameObject.FindObjectOfType<DialogueManager>();
    }

    private IEnumerator playConversation(Chat c)
    {
        if (important)
        {
            StartCoroutine(dm.playConversation(chat.convo));
            cm.Pause();
        }

        //Wait for dialogue to finish
        while (dialoguebox.activeSelf)
        {
            yield return null;
        }

        if (fi != null)
        {
            StartCoroutine(fi.fade());
        }
        cm.Unpause();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hastriggered&&other.CompareTag("Player")|| (other.CompareTag("PlayerHat") && !hastriggered && triggerablebyhat))
        {
            StartCoroutine(playConversation(chat));
            hastriggered = true;
        }
    }

    //triggers on stay to allow triggers to be placed on the player's start point
    private void OnTriggerStay(Collider other)
    {
        if (!hastriggered && other.CompareTag("Player")|| (other.CompareTag("PlayerHat") && !hastriggered && triggerablebyhat))
        {
            StartCoroutine(playConversation(chat));
            hastriggered = true;
        }
    }
}
