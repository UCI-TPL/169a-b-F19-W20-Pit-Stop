﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenePortraits : MonoBehaviour
{
    [SerializeField] private Image portrait1;
    [SerializeField] private Image MustangPortrait1;
    public string portrait1name="";
    [SerializeField] private Image portrait2;
    [SerializeField] private Image MustangPortrait2;
    public string portrait2name="";
    [SerializeField]private int recent = 0;
    [SerializeField] private List<Dialogue> speakersandsprites;
    // Start is called before the first frame update

    public void UpdatePortraits(string newportraitname)
    {
        if(newportraitname.Equals(portrait1name))
        {
            recent = 1;
        }
        else if( newportraitname.Equals(portrait2name)){
            recent = 2;
        }
        else
        {
            Sprite newportrait = FindPortrait(newportraitname);
            if (newportrait == null)
            {
                Debug.Log("No portrait found for char " + newportraitname);
                return;
            }
            if (recent == 0||recent==2)
            {
                if (newportraitname.Equals("Mustang"))
                {
                    MustangPortrait1.gameObject.SetActive(true);
                    portrait1.gameObject.SetActive(false);
                    MustangPortrait1.sprite = newportrait;
                }
                else
                {
                    MustangPortrait1.gameObject.SetActive(false);
                    portrait1.gameObject.SetActive(true);
                    portrait1.sprite = newportrait;
                }

                portrait1name = newportraitname;
                recent = 1;
            }
            else if (recent == 1)
            {
                if (newportraitname.Equals("Mustang"))
                {
                    MustangPortrait2.gameObject.SetActive(true);
                    portrait1.gameObject.SetActive(false);
                    MustangPortrait2.sprite = newportrait;
                }
                else
                {
                    MustangPortrait2.gameObject.SetActive(false);
                    portrait2.gameObject.SetActive(true);
                    portrait2.sprite = newportrait;
                }

                portrait2name = newportraitname;
                recent = 2;
            }
        }
    }

    private Sprite FindPortrait(string portraitname)
    {
        foreach (Dialogue d in speakersandsprites)
        {
            if (d.speaker.Equals(portraitname))
            {
                return d.Scenesprite;
            }
        }
        return null;
    }
}
