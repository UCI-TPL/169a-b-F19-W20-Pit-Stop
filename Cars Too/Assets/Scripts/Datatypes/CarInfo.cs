using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This scriptable object is meant to simplify the number of scriptable objects on the car class, and is simply a compilation of all necessary scriptable objects for the overall car class
[CreateAssetMenu(fileName = "CarInfo.asset", menuName = "Capstone/CarInfo")]
public class CarInfo : ScriptableObject
{
    public BaseStats carbasestats;
    public List<AbilityBaseStats> abilities;
    public List<LevelUpBonuses> levelbonuses;
}
