using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To be used mainly in the creation of chats, or conversations where there is only one set of choices.
[CreateAssetMenu(fileName = "Chat.asset", menuName = "Capstone/Chat")]
public class Chat : Conversation
{
    public string Choice1;
    public string Choice2;

    public List<Dialogue> Route1;
    public List<Dialogue> Route2;
}
