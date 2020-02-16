using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueLog : MonoBehaviour
{
    private string textlog = "";
    [SerializeField] private TextMeshProUGUI log;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        log.text = textlog;
    }

    public void UpdateLog(string n, string d)
    {
        if (textlog != "")
        {
           // textlog += "\n";
        }
        textlog =n + "\n" + d+"\n"+"\n"+textlog;    
    }
}
