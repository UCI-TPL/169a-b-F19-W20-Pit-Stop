﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScenePortraits : MonoBehaviour
{
    [SerializeField] private Sprite defaultimg;
    [SerializeField] private Image portrait1;
    [SerializeField] private Image portrait1mouth;
    [SerializeField] private Image portrait1eyes;
    [SerializeField] private Image MustangPortrait1;
    public string portrait1name="";
    [SerializeField] private Image portrait2;
    [SerializeField] private Image portrait2mouth;
    [SerializeField] private Image portrait2eyes;
    [SerializeField] private Image MustangPortrait2;
    public string portrait2name="";
    [SerializeField]private int recent = 0;
    [SerializeField] private List<Dialogue> speakersandsprites;
    [SerializeField] private Blinking b1;
    [SerializeField] private Blinking b2;
    [SerializeField] private TextMeshProUGUI name1;
    [SerializeField] private TextMeshProUGUI name2;
    [SerializeField] private GameObject n1holder;
    [SerializeField] private GameObject n2holder;


    // Start is called before the first frame update

    private void Start()
    {
        portrait1.gameObject.SetActive(false);
        portrait2.gameObject.SetActive(false);
        MustangPortrait1.gameObject.SetActive(false);
        MustangPortrait2.gameObject.SetActive(false);
    }

    public void UpdatePortraits(string newportraitname, Sprite neweyes = null, Sprite newmouth = null, Sprite newblink = null, string leavename =null)
    {
        if (leavename.Equals(portrait1name))
        {
            MustangPortrait1.gameObject.SetActive(false);
            portrait1.gameObject.SetActive(false);
            portrait1mouth.gameObject.SetActive(false);
            portrait1eyes.gameObject.SetActive(false);
            portrait1name = "";
        }
        else if (leavename.Equals(portrait2name))
        {
            MustangPortrait2.gameObject.SetActive(false);
            portrait2.gameObject.SetActive(false);
            portrait2mouth.gameObject.SetActive(false);
            portrait2eyes.gameObject.SetActive(false);
            portrait2name = "";
        }

        if(newportraitname.Equals(portrait1name))
        {
            recent = 1;
            UpdateExpression(1, newmouth, neweyes,newblink);
            n1holder.SetActive(true);
            n2holder.SetActive(false);
            name1.text = portrait1name;
        }
        else if( newportraitname.Equals(portrait2name)){
            recent = 2;
            UpdateExpression(2, newmouth, neweyes, newblink);
            n2holder.SetActive(true);
            n1holder.SetActive(false);
            name2.text = portrait2name;
        }
        else if (newportraitname.Equals("Lightning"))
        {
            if (!portrait2.gameObject.activeSelf) {
                portrait2.gameObject.SetActive(true);
                portrait2eyes.sprite = defaultimg;
                portrait2mouth.sprite = defaultimg;
                portrait2.sprite = defaultimg;
            }
            n2holder.SetActive(true);
            n1holder.SetActive(false);
            name2.text = DataManager.instance.GetName();
        }
        else
        {
            Sprite newportrait = FindPortrait(newportraitname);
            if (newportrait == null)
            {
                Debug.Log("No portrait found for char " + newportraitname);

                if (!portrait2.gameObject.activeSelf)
                {
                    portrait2.gameObject.SetActive(true);
                    portrait2eyes.sprite = defaultimg;
                    portrait2mouth.sprite = defaultimg;
                    portrait2.sprite = defaultimg;
                }
                n2holder.SetActive(true);
                n1holder.SetActive(false);
                name2.text = newportraitname;
                
                return;
            }
            if (recent == 0||recent==2)
            {
                
                if (newportraitname.Equals("Mustang"))
                {
                    MustangPortrait1.gameObject.SetActive(true);
                    portrait1.gameObject.SetActive(false);
                    portrait1mouth.gameObject.SetActive(false);
                    portrait1eyes.gameObject.SetActive(false);
                    MustangPortrait1.sprite = newportrait;
                }
                else
                {
                    MustangPortrait1.gameObject.SetActive(false);
                    portrait1.gameObject.SetActive(true);
                    portrait1.sprite = newportrait;
                    portrait1mouth.gameObject.SetActive(true);
                    portrait1eyes.gameObject.SetActive(true);

                    if (neweyes == null)
                    {
                        
                        portrait1eyes.sprite = FindEyes(newportraitname);
                    }
                    else
                    {
                        portrait1eyes.sprite = neweyes;
                    }
                    if(newmouth == null)
                    {
                        
                        portrait1mouth.sprite = FindMouth(newportraitname);
                    }
                    else
                    {
                        portrait1mouth.sprite = newmouth;
                    }

                    if (newblink == null)
                    {
                        b1.blinkstate = FindBlink(newportraitname);
                    }
                    else
                    {
                        b1.blinkstate = newblink;
                    }


                }

                portrait1name = newportraitname;
                recent = 1;
                n1holder.SetActive(true);
                n2holder.SetActive(false);
                name1.text = portrait1name;
            }
            else if (recent == 1)
            {
                if (newportraitname.Equals("Mustang"))
                {
                    MustangPortrait2.gameObject.SetActive(true);
                    portrait2.gameObject.SetActive(false);
                    portrait2mouth.gameObject.SetActive(false);
                    portrait2eyes.gameObject.SetActive(false);
                    MustangPortrait2.sprite = newportrait;
                }
                else
                {
                    MustangPortrait2.gameObject.SetActive(false);
                    portrait2.gameObject.SetActive(true);
                    portrait2.sprite = newportrait;
                    portrait2mouth.gameObject.SetActive(true);
                    portrait2eyes.gameObject.SetActive(true);

                    if (neweyes == null)
                    {
                        portrait2eyes.sprite = FindEyes(newportraitname);
                    }
                    else
                    {
                        portrait2eyes.sprite = neweyes;
                    }
                    if (newmouth == null)
                    {
                        portrait2mouth.sprite = FindMouth(newportraitname);
                    }
                    else
                    {
                        portrait2mouth.sprite = newmouth;
                    }

                    if(newblink == null)
                    {
                        b2.blinkstate = FindBlink(newportraitname);
                    }
                    else
                    {
                        b2.blinkstate = newblink;
                    }
                }

                portrait2name = newportraitname;
                recent = 2;
                n2holder.SetActive(true);
                n1holder.SetActive(false);
                name2.text = portrait2name;
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

    private Sprite FindEyes(string portraitname)
    {
        foreach (Dialogue d in speakersandsprites)
        {
            if (d.speaker.Equals(portraitname))
            {
                return d.expression.eyes;
            }
        }
        return null;
    }

    private Sprite FindMouth(string portraitname)
    {
        foreach (Dialogue d in speakersandsprites)
        {
            if (d.speaker.Equals(portraitname))
            {
                return d.expression.mouth;
            }
        }
        return null;
    }

   private Sprite FindBlink(string portraitname)
    {
        foreach (Dialogue d in speakersandsprites)
        {
            if (d.speaker.Equals(portraitname))
            {
                return d.expression.blink;
            }
        }
        return null;
    }

    private void UpdateExpression(int p, Sprite mouth, Sprite eyes, Sprite blink)
    {
        if (mouth != null)
        {
            if (p == 1)
            {
                portrait1mouth.sprite = mouth;
            }
            else
            {
                portrait2mouth.sprite = mouth;
            }
        }
        if (eyes != null)
        {
            if (p == 1)
            {
                portrait1eyes.sprite = eyes;
            }
            else
            {
                portrait2eyes.sprite = eyes;
            }
        }

        if (blink != null)
        {
            if (p == 1)
            {
                b1.blinkstate = blink;
            }
            else
            {
                b2.blinkstate = blink;
            }
        }
    }
}
