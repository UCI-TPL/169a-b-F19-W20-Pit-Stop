using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TooltipText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tooltip = null; //text where tooltip goes
    [SerializeField] private GameObject tooltipobjs = null; //empty that contains all tooltip parts for setting on and off
    // Start is called before the first frame update
    void Start()
    {
        tooltipobjs.SetActive(false);
    }

    public void DisplayTooltip(string txt)
    {
        tooltipobjs.SetActive(true);
        tooltip.text = txt;
    }

    public void TurnOffTooltip()
    {
        tooltipobjs.SetActive(false);
    }
}
