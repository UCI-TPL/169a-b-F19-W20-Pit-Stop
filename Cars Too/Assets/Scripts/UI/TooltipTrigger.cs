using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipTrigger : MonoBehaviour
{
    [SerializeField] private string tooltip = "WASD to MOVE";
    private TooltipText tt = null;

    private void Start()
    {
        if (tt == null)
        {
            tt = GameObject.FindObjectOfType<TooltipText>();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tt.DisplayTooltip(tooltip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tt.TurnOffTooltip();
        }
    }

   
}
