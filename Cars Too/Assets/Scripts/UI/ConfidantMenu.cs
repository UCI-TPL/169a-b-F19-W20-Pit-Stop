﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfidantMenu : MonoBehaviour
{
    public GameObject confidantmenu;
    public Image confidant;
    public Image confidanteyes;
    public Image confidantmouth;
    public Image Mustangconfidant;
    public TextMeshProUGUI nametext;
    public NPC currentNPC;
    public GameObject giftingmenu;
    public GameObject confidantbasemenu;
    public int menuthemeindex = 0;
    public Image affinityup;
    [SerializeField] private AudioClip affinityupsfx;
    public GameObject hidegifts;
    public GameObject hidedate;
    public TextMeshProUGUI confidantleveltext;
    private int maxconfidantlevel = 5;
    private DialogueManager dm;
    public HangoutLocker hl = null;
    [SerializeField] private Image bg;
    [SerializeField] public GameObject npcchaticon;//chat popup to be displayed when close to an npc
    [SerializeField] private Blinking b;


    private void Start()
    {
        if (bg != null)
        {
            bg.gameObject.SetActive(false);
        }
        confidantmenu.SetActive(false);
        dm = GameObject.FindObjectOfType<DialogueManager>();
    }

    public void OpenMenu(NPC npc, Sprite s, string n)
    {
        currentNPC = npc;
        DataManager.instance.am.PlayandTrackBGM(menuthemeindex);
        dm.HaltDialogue();

        confidantmenu.SetActive(true);
        confidantbasemenu.SetActive(true);
        SetConfidantPortrait(n, s, npc.defaultexp);
        giftingmenu.SetActive(false);
        nametext.text = n;
        confidant.sprite = s;
        hl.IsLocked(n);
        UpdateConfidantBar();
        bg.gameObject.SetActive(true);
        bg.sprite = npc.npcbg;
        /*
        if (DataManager.instance.GetConfidantLevel(npc.Confidantname) >= 1)
        {
            hidegifts.SetActive(false);
        }
        else
        {
            hidegifts.SetActive(true);
        }
        if (DataManager.instance.GetConfidantLevel(npc.Confidantname) >= 4)
        {
            hidedate.SetActive(false);
        }
        else
        {
            hidedate.SetActive(true);
        }
        */
    }

    public void CloseMenuButton()
    {
        currentNPC.closeMenu();
    }

    //DONT Use this one on the button
    public void CloseMenu(bool imageclosed = true)
    {
        confidantmenu.SetActive(false);
        affinityup.gameObject.SetActive(false);
        if (imageclosed)
        {
            DataManager.instance.am.PlayCurrentTheme();
            TurnOffPortrait();
            bg.gameObject.SetActive(false);
        }
    }

    public void OpenGiftingMenu()
    {
        confidantbasemenu.SetActive(false);
        hidedate.SetActive(false);
        giftingmenu.SetActive(true);
        dm.DisplayLine(currentNPC.giftingchat.convo[Random.Range(0, currentNPC.giftingchat.convo.Count)]);

    }

    public void CloseGiftingMenu()
    {
        confidantbasemenu.SetActive(true);
        /*
        if (DataManager.instance.GetConfidantLevel(currentNPC.Confidantname) >= 4)
        {
            hidedate.SetActive(false);
        }
        else
        {
            hidedate.SetActive(true);
        }
        */
        UpdateConfidantBar();
        giftingmenu.SetActive(false);
        dm.DisplayLine(currentNPC.idlechats.convo[Random.Range(0, currentNPC.idlechats.convo.Count)]);
    }

    public void GiveGift(PresentType present)
    {
        currentNPC.RecieveGift(present);
    }

    public IEnumerator AffinityIcon()
    {
        affinityup.gameObject.SetActive(true);
        DataManager.instance.am.PlaySound(affinityupsfx);
        //make opaque
        affinityup.color = new Color(affinityup.color.r, affinityup.color.b, affinityup.color.g, 1.0f);
        float i = 1.0f;
        float activeframes = 180.0f;
        float incrementer = i / activeframes;
        while (i > 0)
        {
            affinityup.color = new Color(affinityup.color.r, affinityup.color.b, affinityup.color.g, i);
            yield return null;
            i -= incrementer;
        }
        affinityup.gameObject.SetActive(false);
    }

    public void DateNPC()
    {
        currentNPC.PlayDate();
    }

    private void UpdateConfidantBar()
    {
        //Image i =confidantbar.GetComponent<Image>();
        //i.fillAmount = DataManager.instance.GetConfidantLevel(currentNPC.Confidantname)/maxconfidantlevel;
        confidantleveltext.text = DataManager.instance.GetConfidantLevel(currentNPC.Confidantname) + 1 + "/" + maxconfidantlevel;
    }

    private void SetConfidantPortrait(string n, Sprite s, Expression e)
    {
        if (n.Equals("Mustang"))
        {
            Mustangconfidant.gameObject.SetActive(true);
            Mustangconfidant.sprite = s;
            confidanteyes.gameObject.SetActive(false);
            confidantmouth.gameObject.SetActive(false);
        }
        else
        {
            confidant.gameObject.SetActive(true);
            confidanteyes.gameObject.SetActive(true);
            confidantmouth.gameObject.SetActive(true);
            confidant.sprite = s;
            confidanteyes.sprite = e.eyes;
            confidantmouth.sprite = e.mouth;
            b.blinkstate = e.blink;

        }
    }

    private void TurnOffPortrait()
    {
        confidant.gameObject.SetActive(false);
        Mustangconfidant.gameObject.SetActive(false);
        confidanteyes.gameObject.SetActive(false);
        confidantmouth.gameObject.SetActive(false);
    }

    public void UpdateConfidant(Expression e)
    {
        confidanteyes.sprite = e.eyes;
        confidantmouth.sprite = e.mouth;
        b.blinkstate = e.blink;
    }
}
