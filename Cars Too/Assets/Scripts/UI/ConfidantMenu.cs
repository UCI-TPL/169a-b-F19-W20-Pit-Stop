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

    private void Start()
    {
        confidantmenu.SetActive(false);
    }

    public void OpenMenu(NPC npc,Sprite s, string n)
    {
        confidant.gameObject.SetActive(true);
        confidantmenu.SetActive(true);
        confidantbasemenu.SetActive(true);
        giftingmenu.SetActive(false);
        nametext.text = n;
        confidant.sprite = s;
        currentNPC = npc;
    }

    public void CloseMenuButton()
    {
        currentNPC.closeMenu();
    }

    //DONT Use this one on the button
    public void CloseMenu(bool imageclosed=true)
    {
        confidantmenu.SetActive(false);
        if(imageclosed)
            confidant.gameObject.SetActive(false);
    }

    public void OpenGiftingMenu()
    {
        confidantbasemenu.SetActive(false);
        giftingmenu.SetActive(true);
    }

    public void CloseGiftingMenu()
    {
        confidantbasemenu.SetActive(true);
        giftingmenu.SetActive(false);
    }

    public void GiveGift(PresentType present)
    {
        currentNPC.RecieveGift(present);
    }
}
