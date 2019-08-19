using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{

    public LevelUpBonuses bonuses;
    public int currentXp = 0;

    public int maxXp = 0;

    public int currentLevel = 0;

    public int maxLevel = 0;    

    public void Start() {
        bonuses = FindObjectOfType<LevelUpBonuses>();
    }

    public void increaseXp(int amount) {
        if((currentXp + amount) >= maxXp) {
            currentXp = maxXp;
        } else {
            currentXp += amount;
        }
    }

    public void LevelUp() {
        if(currentLevel == maxLevel) {
            return;
        } else {
            currentLevel += 1;

            // Here goes the bonus increases
        }
    }
}
