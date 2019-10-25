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

    public void OpenMenu(NPC npc,Sprite s, string n)
    {
        confidant.gameObject.SetActive(true);
        confidantmenu.SetActive(true);
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
}
