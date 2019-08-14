using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This scriptable object designates what changes occur at each level including stat upgrades, new abilities, and ability upgrades
[CreateAssetMenu(fileName = "LevelUpBonuses.asset", menuName = "Capstone/LevelUpBonuses")]
public class LevelUpBonuses : ScriptableObject
{
    //Stat changes to be added to the cars current stats
    public float powerchange = 0;
    public float enginechange = 0;
    public float handlingchange = 0;
    public float weightchange = 0;
    public float hpchange = 10;
    public float armorchange = 0;

    //The name of the new ability to be added to available abilities, if null none is added
    public string newability = null;

    //the name of the ability to be upgraded, if null none is upgraded
    public string upgradedability = null;

}
