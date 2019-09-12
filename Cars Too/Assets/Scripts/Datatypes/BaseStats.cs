using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base Stats is a Scriptable Object class that stores base values, and other preset values for cars, this is used to maintain a constant baseline whenever the game is started for each class.

[CreateAssetMenu(fileName = "BaseStats.asset", menuName = "Capstone/BaseStats")]
public class BaseStats : ScriptableObject
{
    public GameObject carmodel;

    public float power = 10;
    public float engine = 10;
    public float handling = 10;
    public float weight = 10;
    public float hp = 100;
    public float armor = 10;


    [TextArea(3, 100)]
    public string description;
    

}
