using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    CarStats cs;

    //initializes stats based off of the stats stored in carstats
    void Start()
    {
        cs = GameObject.FindObjectOfType<CarStats>();
        stats = cs.stats;
        cs.SpawnEquippedAbilities(this.gameObject);

    }

    //Add Exp here after defeating enemies, if a levelup occurs, propagates it to the stats and abilities on carstats
    //potentially add ui popups of levelupbonuses here
    public void AddExp(float amt)
    {
        bool levelup=cs.exp.AddExp(amt);
        if (levelup)
        {
            int level = cs.exp.level-1;
            stats.UpgradeStats(cs.carinfo.levelbonuses[level]);
            cs.stats.UpgradeStats(cs.carinfo.levelbonuses[level]);
            cs.abils.UpgradeandUnlock(cs.carinfo.levelbonuses[level]);
        }
    }
    
    //Send Game Over message and stop the game
    public override void Die()
    {
        Debug.Log("Player has Died");
    }

    
}
