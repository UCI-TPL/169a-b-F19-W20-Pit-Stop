using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This holds the base stats for each ability as well as the ability's prefab which will be loaded on entering the game.
[CreateAssetMenu(fileName = "AbilityBaseStats.asset", menuName = "Capstone/AbilityBaseStats")]
public class AbilityBaseStats : ScriptableObject
{
    //base damage modifier for the ability
    public float damagemodifier = 2.5f;

    //the amount that the ability's damage modifier is upgraded each time it levels up
    public float upgradeamt = .5f;

    //the ability prefab that is loaded on entering the game, and has its own scripts to facillitate its behavior
    public GameObject abilityprefab;

}
