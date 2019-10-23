using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To be used mainly in the creation of chats, or conversations where there is only one set of choices.
[CreateAssetMenu(fileName = "Chat.asset", menuName = "Capstone/Chat")]
public class Chat : Conversation
{
    //Text that will appear on each choice button
    public string Choice1;
    public string Choice2;

    //Dialogue for each choice
    public List<Dialogue> Route1;
    public List<Dialogue> Route2;

    //Hopefully I can get rid of this at some point,
    //but this exist basically just to allow you to put conversations where there
    //is usually chats as its difficult to scale it back down without some sort of flag
    public bool isConversation = false;
    //Affinity rewarded for each choice
    public int C1Reward = 0;
    public int C2Reward = 0;
}
