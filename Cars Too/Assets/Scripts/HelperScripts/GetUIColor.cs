using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GetUIColor : MonoBehaviour
{
    //if the component is an image, whether it is an outline or a bg
    [SerializeField] private bool isoutline = false;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI txt= GetComponent<TextMeshProUGUI>();
        if (txt != null)
        {
            txt.color = DataManager.instance.uicoltext;
        }
        Image i = GetComponent<Image>();
        if (i != null)
        {
            if (isoutline)
            {
                i.color = DataManager.instance.uicoloutline;
            }
            else
            {
                i.color = DataManager.instance.uicol;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
