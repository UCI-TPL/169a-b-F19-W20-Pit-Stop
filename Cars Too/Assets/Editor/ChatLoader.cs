using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

[CustomEditor(typeof(Chatlist))]
public class ChatLoader : Editor
{
    private ExpressionList expl;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Chatlist cl = (Chatlist)target;

        if (GUILayout.Button("Load Chats"))
        {
            LoadChats(cl);
        }
    }

    public void LoadChats(Chatlist cl)
    {
        expl = cl.expl;
        cl.chats = new List<Chat>();
        string readpath = Application.dataPath + cl.fileloc;

        StreamReader sr = new StreamReader(readpath);
        while (true)
        {
            Chat temp = (GetChat(sr));
            if (temp == null)
            {
                break;
            }
            if (!temp.isConversation)
            {
                List<string> cs = getChoices(sr);
                temp.Choice1 = cs[0];
                temp.Choice2 = cs[1];
                temp.Route1 = GetChat(sr).convo;
                temp.Route2 = GetChat(sr).convo;
            }

            AssetDatabase.CreateAsset(temp, cl.createpath + cl.title + (cl.chats.Count + 1) + ".asset");
            EditorUtility.SetDirty(temp);
            cl.chats.Add(temp);
        }

        sr.Close();

    }

    private Chat GetChat(StreamReader sr)
    {
        Chat output = new Chat();
        output.convo = new List<Dialogue>();
        string current = "";
        List<Dialogue> dl = new List<Dialogue>();
        string currentspeaker = "";
        string currentdialogue = "";

        //Continue until the End is reached
        while (!sr.EndOfStream)
        {
            //read next line
            current = sr.ReadLine();

            if (current == "End")
            {
                output.isConversation = true;
                break;
            }
            else if (current == "EndC")
            {
                output.isConversation = false;
                break;
            }
            //If current is a blank line it might indicate the end of the dialogue block or 
            //That we have yet to reach our speaker
            if (current.Equals(""))
            {

                if (!currentdialogue.Equals("") && !currentspeaker.Equals(""))
                {

                    dl.Add(new Dialogue(currentspeaker, currentdialogue));
                    if (!sr.EndOfStream)
                    {
                        getExpression(sr, dl[dl.Count-1], false);
                        if (!sr.EndOfStream)
                        {
                            getExpression(sr, dl[dl.Count - 1], true);
                        }
                    }
                    currentspeaker = "";
                    currentdialogue = "";
                }



            }
            //If we haven't defined our speaker yet, it should be the next nonemptyline
            else if (currentspeaker.Equals(""))
            {
                currentspeaker = current.Substring(0, current.Length - 1);
                //remove the : from the speaker

            }
            else
            {
                //Otherwise we want to add this line to our dialogue
                currentdialogue += current;

            }
        }


        if (dl.Count == 0)
        {
           
            return null;
        }
        output.convo = dl;


        return output;
    }

    //checks if there are still lines to be read
    private List<string> getChoices(StreamReader sr)
    {
        List<string> choices = new List<string>();
        string current = "";
        while (choices.Count < 2)
        {
            current = sr.ReadLine();
            if (!current.Equals("") && !current.Equals("Choices"))
            {
                
                choices.Add(current);
            }

        }

        return choices;
    }

    private void getExpression(StreamReader sr, Dialogue d, bool second)
    {
        
        if (sr.Peek() == '$')
        {
            Debug.Log("$ detected");
            string e =sr.ReadLine();
            Expression expr = null;
            int eindex = int.Parse(e[2].ToString());
            if (e[1] == 'P' || e[1] == 'p')
            {
                
               
                expr = expl.PiperExp[eindex];
            }
            else if (e[1] == 'M' || e[1] == 'm')
            {

                expr = expl.MustangExp[eindex];

            }
            else if (e[1] == 'C' || e[1] == 'c')
            {

                expr = expl.ChiefExp[eindex];
            }
            else if (e[1] == 'D' || e[1] == 'd')
            {

                expr = expl.DexExp[eindex];
            }
            else if (e[1] == 'S' || e[1] == 's')
            {

                expr = expl.SpringtrapExp[eindex];
            }

            if (second || e.Length >= 4 && e[3] == '2')
            {
                d.expression2 = expr;
            }
            else
            {
                d.expression = expr;
            }

            sr.ReadLine();

        }
        //return null;
    }
}
