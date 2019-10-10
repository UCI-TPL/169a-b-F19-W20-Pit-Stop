using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dialogue :MonoBehaviour
{
    public string speaker = "Name";
    public string text = "Dialogue";

    public Dialogue(string spk, string txt)
    {
        speaker = spk;
        text = txt;

    }
}
  
