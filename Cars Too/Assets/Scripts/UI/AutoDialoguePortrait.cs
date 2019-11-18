using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AutoDialoguePortrait : MonoBehaviour
{
    [SerializeField] private Image portrait = null; //image in scene where portraits will be displayed
    [SerializeField] List<Dialogue> speakersandsprites= null; //list of all char portrait sprites

    public void ShowPortrait(string charname)
    {
        foreach (Dialogue d in speakersandsprites)
        {
            if (d.speaker.Equals(charname))
            {
                portrait.sprite = d.Scenesprite;
                break;
            }
        }
    }
}
