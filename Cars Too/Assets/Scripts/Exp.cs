using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp 
{
    public float currentxp = 0.0f;
    public float maxxp = 100.0f;
    public int level = 1;

    //Adjustable numbers that determine the max level, and the increase of the exp requirement per level
    float xpincrementer = 1.5f;
    int maxlevel = 10;

    //Adds Xp, returns true if a levelup occured as a result
    //Otherwise returns false
    public bool AddExp(float amt)
    {
        if (MaxLevel())
        {
            return false;
        }

        currentxp += amt;

        //Checks for levelup
        if (currentxp >= maxxp)
        {
            //checks if maxlevel has been reached
            level++;
            if (MaxLevel())
            {
                currentxp = 0;
                maxxp = 0;
            }
            else
            {
                //if not at maxlevel, maintains excess xp, and increments max xp
                currentxp -= maxxp;
                maxxp *= xpincrementer;
            }
            return true;
        }


        return false;
    }



    public bool MaxLevel()
    {
        return level == maxlevel;
    }

}
