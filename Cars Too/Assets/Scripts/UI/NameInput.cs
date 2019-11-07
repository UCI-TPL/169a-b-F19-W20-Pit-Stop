using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameInput : MonoBehaviour
{
    public GameObject nameInputPanel;
    public TextMeshProUGUI playerName;

    // Start is called before the first frame update
    void Start()
    {
        DisableNameInput();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerName()
    {
        if(playerName.text != "")
        {
            DataManager.instance.SetName(playerName.text);
        }
        
    }
    
    public void EnableNameInput()
    {
        nameInputPanel.SetActive(true);
    }

    public void DisableNameInput()
    {
        nameInputPanel.SetActive(false);
    }
}
