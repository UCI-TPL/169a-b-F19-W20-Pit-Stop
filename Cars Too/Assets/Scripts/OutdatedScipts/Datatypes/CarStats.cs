using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the overall container for all of the stats for the car including abilities, stats, Exp, Levelupboost
//This will be put on a don't destroy on load gameobject, and used to store and access carstats between scenes
public class CarStats : MonoBehaviour
{
    public CarInfo carinfo;
    public Stats stats;
    public Exp exp;
    public Abilities abils;

    //Initializes exp, stats, and abilities based off of carinfo
    private void Awake()
    {
        exp = new Exp();
        stats = new Stats();
        abils = new Abilities();

        abils.InstantiateAbilities(carinfo.abilities);
        stats.InitializeStats(carinfo.carbasestats);

    }

    //Spawns the equipped abilities at given target object
    public void SpawnEquippedAbilities(GameObject playercar)
    {
        foreach(Ability a in abils.equipped)
        {
           GameObject temp= Instantiate(a.abilityprefab, playercar.transform);
            temp.GetComponent<AbilityEnabler>().setAbility(a);
        }
    }

}
