using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfidantData 
{
    int affinity = 0;
    string confidantname= "";
    [SerializeField] List<int> clvlthresholds =new List<int>();
    public bool met = false;
    public List<int> chatindex=new List<int>();
    public int dateindex = 0;
    

     public ConfidantData(string s)
    {
        affinity = 0;
        confidantname =s;
        initlvlthresholds();
        chatindex.Add(0);
        chatindex.Add(0);
        chatindex.Add(0);
    }

    public ConfidantData(int i, string s)
    {
        affinity = i;
        confidantname = s;
        initlvlthresholds();
        chatindex.Add(0);
        chatindex.Add(0);
        chatindex.Add(0);
    }


    private void initlvlthresholds()
    {
        clvlthresholds.Add(50);
        clvlthresholds.Add(150);
        clvlthresholds.Add(225);
        clvlthresholds.Add(350);
        clvlthresholds.Add(500);
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
        
        affinity += (int)(i*GetAffinityMultiplier());
        //Debug.Log((int)(i * GetAffinityMultiplier()));
        return GetConfidantLevel() > temp;
    }

    private float GetAffinityMultiplier()
    {
        if (DataManager.instance.affinityBoost)
        {
            return 1.3f;
        }
            return 1.0f;
        
    }

    public int getchatindex()
    {
        return chatindex[DataManager.instance.phase];
    }
    public void incrementchatindex()
    {
        chatindex[DataManager.instance.phase]++;
    }

    public void resetchatindex()
    {
        chatindex[DataManager.instance.phase]=0;
    }



    public void MetConfidant()
    {
        met = true;
    }
}
