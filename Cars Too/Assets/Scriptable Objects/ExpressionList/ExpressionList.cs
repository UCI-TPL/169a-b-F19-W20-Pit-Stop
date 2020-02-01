using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EXPL.asset", menuName = "Capstone/ExpressionList")]
public class ExpressionList : ScriptableObject
{
    public List<Expression> PiperExp;
    public List<Expression> MustangExp;
    public List<Expression> DexExp;
    public List<Expression> SpringtrapExp;
    public List<Expression> ChiefExp;
}
