using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Chatlist.asset", menuName = "Capstone/Chatlist")]
public class Chatlist : ScriptableObject
{
    public List<Chat> chats;
    //path from assets to txt file
    public string fileloc;
    //path to folder where new chats will be generated
    [TextArea(5,100)]
    public string createpath;
    //generic title all chats will be indexed under
    public string titles;

    public void LoadChats()
    {
        
        string readpath = Application.dataPath + fileloc;
        
        StreamReader sr = new StreamReader(readpath);
        Chat temp =(GetChat(sr));
        //Debug.Log(temp.convo[0].text);
        //chats[0].convo = new List<Dialogue>();
        AssetDatabase.CreateAsset(temp,createpath);

        foreach (Dialogue d in temp.convo)
        {
            //chats[0].convo.Add(d);
        }

        foreach (Chat c in chats){
            foreach(Dialogue d in c.convo)
            {
                Debug.Log(d.speaker);
                Debug.Log(d.text);
            }
        }
    }

    private Chat GetChat(StreamReader sr)
    {
        Chat output = new Chat();
        string current = "";
        List<Dialogue> dl = new List<Dialogue>();
        string currentspeaker = "";
        string currentdialogue= "";

         //Continue until the End is reached
        while (current != "End")
        {
            //read next line
            current = sr.ReadLine();

            if (current == "End")
            {
                break;
            }
            //If current is a blank line it might indicate the end of the dialogue block or 
            //That we have yet to reach our speaker
            if (current.Equals(""))
            {
                
                if (!currentdialogue.Equals("")&& !currentspeaker.Equals(""))
                {
                    
                    dl.Add(new Dialogue(currentspeaker, currentdialogue));
                    currentspeaker = "";
                    currentdialogue = "";
                }
               
            }
            //If we haven't defined our speaker yet, it should be the next nonemptyline
            else if (currentspeaker.Equals(""))
            {
                currentspeaker = current.Substring(0,current.Length-1);
                //remove the : from the speaker
                
            }
            else
            {
                //Otherwise we want to add this line to our dialogue
                currentdialogue += current;

            }
        }

        output.convo = dl;
        output.isConversation = true;

        return output;
    }
}
