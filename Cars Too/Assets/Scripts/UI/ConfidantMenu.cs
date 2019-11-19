using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfidantMenu : MonoBehaviour
{
    public GameObject confidantmenu;
    public Image confidant;
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
    [SerializeField] private Image bg;

    private void Start()
    {
        bg.gameObject.SetActive(false);
        confidantmenu.SetActive(false);
        dm = GameObject.FindObjectOfType<DialogueManager>();
    }

    public void OpenMenu(NPC npc, Sprite s, string n)
    {
        DataManager.instance.am.PlayandTrackBGM(menuthemeindex);
        dm.HaltDialogue();
        confidant.gameObject.SetActive(true);
        confidantmenu.SetActive(true);
        confidantbasemenu.SetActive(true);
        giftingmenu.SetActive(false);
        nametext.text = n;
        confidant.sprite = s;
        currentNPC = npc;
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
    public void CloseMenu(bool imageclosed=true)
    {
        confidantmenu.SetActive(false);
        affinityup.gameObject.SetActive(false);
        if (imageclosed)
        {
            DataManager.instance.am.PlayCurrentTheme();
            confidant.gameObject.SetActive(false);
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
        confidantleveltext.text = DataManager.instance.GetConfidantLevel(currentNPC.Confidantname)+1 + "/" + maxconfidantlevel;
    }
}
