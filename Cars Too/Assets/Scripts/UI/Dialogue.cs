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
    public Expression expression2 = null; // changes the expression of the second character in a scene
    public string leavename = null;
    public AudioClip voiceline = null;
    public Sprite newbg = null;
    

    public Dialogue(string spk, string txt)
    {
        speaker = spk;
        text = txt;

    }
}
  
