using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacking : Id
{
    bool close = false;
    [SerializeField] private GameObject hackingui;
    [SerializeField] private GameObject canthack;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (hackingui.activeSelf)
        {
            hackingui.SetActive(false);
        }
        if (canthack.activeSelf)
        {
            canthack.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (close&&Input.GetKeyDown(KeyCode.E)&&DataManager.instance.canHack)
        {
            //On being hacked add its id to the datamanager
            DataManager.instance.AddID(GetID());
            this.gameObject.SetActive(false);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            close = true;
            if (DataManager.instance.canHack)
            {
                hackingui.SetActive(true);
            }
            else
            {
                canthack.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            close = false;
            if (DataManager.instance.canHack)
            {
                hackingui.SetActive(false);
            }
            else
            {
                canthack.SetActive(false);
            }
        }
    }
}
