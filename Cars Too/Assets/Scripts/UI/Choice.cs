using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Choice.asset", menuName = "Capstone/Choice")]
public class Choice : Conversation
{
    //To be used in Cutscenes and any conversation where there are two or more sets of choices
    //The main difference here is that choice can be reinserted again as either route 1 and route 2
    //allowing for a bit more detailed input but being a bit more complicated. 
    //*Note: A conversation will end after one of the routes a player goes down is not another choice

    public string Choice1;
    public string Choice2;

    public Conversation Route1;
    public Conversation Route2;
}
