using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The stats class stores a "live version" of player stats that is manipulated by level up bonuses, and potentially temporarily by abilities
public class Stats : MonoBehaviour
{
    //The stats are stored as public for easier access, but cannot be statics as that would interfere with other class's stats
    public float currenthp = 100;
    public float power = 10;
    public float engine = 10;
    public float handling = 10;
    public float weight = 10;
    public float hp = 100;
    public float armor = 10;

    //initializes the stats class to the basestats given
    public void InitializeStats(BaseStats bs)
    {
        power = bs.power;
        engine = bs.engine;
        handling = bs.handling;
        weight = bs.weight;
        currenthp = bs.hp;
        hp = bs.hp;
        armor = bs.armor;
    }

    //Upgrades Stats based on the levelup bonuses provided
    public void UpgradeStats(LevelUpBonuses lb)
    {
        power += lb.powerchange;
        engine += lb.enginechange;
        handling += lb.handlingchange;
        weight += lb.weightchange;
        currenthp += lb.hpchange;
        hp += lb.hpchange;
        armor += lb.armorchange;
    }
}
