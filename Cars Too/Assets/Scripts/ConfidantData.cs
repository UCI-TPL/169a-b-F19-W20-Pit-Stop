using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfidantData 
{
    int affinity = 0;
    string confidantname= "";
    [SerializeField] List<int> clvlthresholds =new List<int>();
    public bool met = false;

     public ConfidantData(string s)
    {
        affinity = 0;
        confidantname =s;
        initlvlthresholds();
    }

    public ConfidantData(int i, string s)
    {
        affinity = i;
        confidantname = s;
        initlvlthresholds();
    }


    private void initlvlthresholds()
    {
        clvlthresholds.Add(25);
        clvlthresholds.Add(100);
        clvlthresholds.Add(250);
        clvlthresholds.Add(450);
        clvlthresholds.Add(700);
    }
    public int GetConfidantLevel()
    {
        Debug.Log(affinity);
        for (int i =0;i<clvlthresholds.Count; i++)
        {
            if (affinity < clvlthresholds[i])
            {
                return i;
            }
        }
        return clvlthresholds.Count;
    }

    //returns true if a levelup occurs
    public bool AddAffinity(int i)
    {
        int temp = GetConfidantLevel();
        affinity += i;
        return GetConfidantLevel() > temp;
    }

    public void MetConfidant()
    {
        met = true;
    }
}
