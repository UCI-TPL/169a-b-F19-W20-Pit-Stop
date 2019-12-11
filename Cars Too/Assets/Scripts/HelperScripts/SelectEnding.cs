using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEnding : MonoBehaviour
{
    //character endings
    public Chatlist Mending;
    public Chatlist Pending;
    public Chatlist Sending;
    public Chatlist Dending;
    public ScenePlayer sp;

    // Start is called before the first frame update
    void Awake()
    {
        string conf = GetHighestConfidant();
        if (!conf.Equals(""))
        {
            if (conf.Equals("Dex"))
            {
                sp.cl = Dending;
            }
            if (conf.Equals("Piper"))
            {
                sp.cl = Pending;
            }
            if (conf.Equals("Springtrap"))
            {
                sp.cl = Sending;
            }
            if (conf.Equals("Mustang"))
            {
                sp.cl = Mending;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string GetHighestConfidant()
    {
        int level = 0;
        string conf = "";
        if (DataManager.instance.GetConfidantLevel("Dex") >= 4 && DataManager.instance.GetConfidantLevel("Dex")>level)
        {
            level = DataManager.instance.GetConfidantLevel("Dex");
            conf = "Dex";
        }
        if (DataManager.instance.GetConfidantLevel("Mustang") >= 4 && DataManager.instance.GetConfidantLevel("Mustang") > level)
        {
            level = DataManager.instance.GetConfidantLevel("Mustang");
            conf = "Mustang";
        }
        if (DataManager.instance.GetConfidantLevel("Springtrap") >= 4 && DataManager.instance.GetConfidantLevel("Springtrap") > level)
        {
            level = DataManager.instance.GetConfidantLevel("Springtrap");
            conf = "Springtrap";
        }
        if (DataManager.instance.GetConfidantLevel("Piper") >= 4 && DataManager.instance.GetConfidantLevel("Piper") > level)
        {
            level = DataManager.instance.GetConfidantLevel("Piper");
            conf = "Piper";
        }

        return conf;
    }
}
