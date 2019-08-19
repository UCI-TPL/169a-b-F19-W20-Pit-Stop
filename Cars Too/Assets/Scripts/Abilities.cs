using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abilities stores what abilities are currently available, and unavailable, as well as which abilities are equipped. 
public class Abilities : MonoBehaviour
{
    //The player's currently equipped abilities
    public List<Ability> equipped;

    //Abilities that are available but not equipped
    public List<Ability> available;
   
    //Abilities that have not been unlocked yet
    public List<Ability> locked;

    public void InstantiateAbilities(List<AbilityBaseStats> abils)
    {
        //Instantiates the ability lists
        equipped = new List<Ability>();
        available = new List<Ability>();
        locked = new List<Ability>();

        //loads in the abilities
        for (int i= 0; i < abils.Count; i++)
        {
            Ability temp = new Ability(abils[i]);
            
            //adds the first three to equipped, and any excess to locked to be unlocked later
            if (i < 3)
            {
                equipped.Add(temp);
            }
            else
            {
                locked.Add(temp);
            }  
        }

    }

    //Swaps two abilities with the given names, assumes that one is in available, and the other is in equipped
    public void SwapAbilities(string abilityname1, string abilityname2)
    {
        //This bool is used to indicate which was found in equipped and thus no longer needs to be searched for
        bool first=true;

        //Searches through equipped for both abilities
        for(int i= 0; i < equipped.Count; i++)
        {
            if (equipped[i].abilityname == abilityname1)
            {
                //adds the abilitiy to the other list then removes it from this one, and sets the bool to the correct value
                available.Add(equipped[i]);
                equipped.RemoveAt(i);
                first = true;
                break;
            }
            else if (equipped[i].abilityname == abilityname2)
            {
                available.Add(equipped[i]);
                equipped.RemoveAt(i);
                first = false;
                break;
            }
        }



        for (int i = 0; i < available.Count; i++)
        {
            if (!first&&available[i].abilityname == abilityname1)
            {
                equipped.Add(available[i]);
                available.RemoveAt(i);
                break;
            }
            else if (first&&available[i].abilityname == abilityname2)
            {
                equipped.Add(available[i]);
                available.RemoveAt(i);
                break;
            }
        }

    }

    //Unlocks and Upgrades abilities based on levelupbonuses
    public void UpgradeandUnlock(LevelUpBonuses lb)
    {
        //if there is a new ability to be added, check all locked abilities and the one with the same name to available
        if (lb.newability != null)
        {
            foreach(Ability a in locked){
                if (a.abilityname == lb.newability)
                {
                    available.Add(a);
                    locked.Remove(a);
                    break;
                }
            }
        }

        //if there is an ability to be upgraded search available and equipped for it, and upgrade it
        if (lb.upgradedability != null)
        {
            foreach(Ability a in available)
            {
                if (a.abilityname == lb.upgradedability)
                {
                    a.Upgrade();
                    break;
                }
            }

            foreach(Ability a in equipped)
            {
                if (a.abilityname == lb.upgradedability)
                {
                    a.Upgrade();
                    break;
                }
            }
        }
    }



}

//Class that holds the "live version" of ability stats that is changeable
public class Ability
{
    public string abilityname = "PLACEHOLDER";
    public float damagemodifier = 2.5f; // placeholder amount
    public float upgradeamt = .5f; // placeholder amount
    public string abilitydescription = "PLACEHOLDER_TEXT";

    public Ability(AbilityBaseStats abs)
    {
        abilityname = abs.abilityname;
        damagemodifier = abs.damagemodifier;
        upgradeamt = abs.upgradeamt;
        abilitydescription = abs.abilitydescription;

    }

    public void Upgrade()
    {
        damagemodifier += upgradeamt;
    }

}