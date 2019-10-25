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

    public void OpenMenu(Sprite s, string n)
    {
        confidant.gameObject.SetActive(true);
        confidantmenu.SetActive(true);
        nametext.text = n;
        confidant.sprite = s;
    }

    public void CloseMenu(bool imageclosed=true)
    {
        confidantmenu.SetActive(false);
        if(imageclosed)
            confidant.gameObject.SetActive(false);
    }
}
