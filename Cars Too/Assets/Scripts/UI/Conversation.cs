using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Convo.asset", menuName = "Capstone/Conversation")]
[System.Serializable]
public class Conversation : ScriptableObject
{
    
    public List<Dialogue> convo=new List<Dialogue>();
    


    /* Old code that converted a list of strings into dialogue
    public List<Dialogue> converttoDialogue()
    {
        List <Dialogue> d = new List<Dialogue>();
        for(int i= 0; i<convo.Count; i++)
        {
            if (i + 1 >= convo.Count)
            {
                Debug.Log("ERROR CONVERSATION NOT SETUP PROPERLY");
                break;
            }
            d.Add(new Dialogue(convo[i], convo[i + 1]));
            i++;
        }


        return d;
    }
    */
}

