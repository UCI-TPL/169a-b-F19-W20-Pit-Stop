using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClick : MonoBehaviour
{
    [SerializeField] GameObject pmenu = null;
    DialogueManager dm = null;

    private void Start()
    {
        dm = GameObject.FindObjectOfType<DialogueManager>();
    }
    // Start is called before the first frame update
    public void PlayerClicked()
    {
        if ((pmenu == null || !pmenu.activeSelf) && !DataManager.instance.recentlyclosed)
        {
            dm.instant = true;
        }
    }
}
