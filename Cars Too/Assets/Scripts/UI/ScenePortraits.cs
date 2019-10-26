using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenePortraits : MonoBehaviour
{
    [SerializeField] private Image portrait1;
    public string portrait1name="";
    [SerializeField] private Image portrait2;
    public string portrait2name="";
    [SerializeField]private int recent = 0;
    // Start is called before the first frame update

    public void UpdatePortraits(string newportraitname, Sprite newportrait)
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
            if (recent == 0||recent==2)
            {
                portrait1.gameObject.SetActive(true);
                portrait1.sprite = newportrait;
                portrait1name = newportraitname;
                recent = 1;
            }
            else if (recent == 1)
            {
                portrait2.gameObject.SetActive(true);
                portrait2.sprite = newportrait;
                portrait2name = newportraitname;
                recent = 2;
            }
        }
    }
}
