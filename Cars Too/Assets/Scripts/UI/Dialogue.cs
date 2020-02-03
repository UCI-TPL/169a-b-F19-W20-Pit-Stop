using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string speaker = "Name";
    [TextArea(5, 100)]
    public string text = "Dialogue";
    public Sprite Scenesprite = null;
    public Expression expression = null;
    public string leavename = null;

    public Dialogue(string spk, string txt)
    {
        speaker = spk;
        text = txt;

    }
}
  
