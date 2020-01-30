using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Expression.asset", menuName = "Capstone/Expression")]
[System.Serializable]
public class Expression : ScriptableObject
{
    public Sprite mouth = null;
    public Sprite eyes = null;
    public Sprite blink = null;

}
