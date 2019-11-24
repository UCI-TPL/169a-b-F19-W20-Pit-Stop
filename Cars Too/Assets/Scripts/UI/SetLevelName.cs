using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetLevelName : MonoBehaviour
{
    [SerializeField] private string LevelName = "The Factory";
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = LevelName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
