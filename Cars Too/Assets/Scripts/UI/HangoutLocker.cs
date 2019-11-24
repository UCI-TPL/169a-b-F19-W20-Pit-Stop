using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangoutLocker : MonoBehaviour
{
    Button b = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void IsLocked(string n)
    {
        if(b==null)
            b = GetComponent<Button>();

        if (DataManager.instance.GetConfidantLevel(n) >= 4)
        {
            b.interactable = true;
        }
        else
        {
            b.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
