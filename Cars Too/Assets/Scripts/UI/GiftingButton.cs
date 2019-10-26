using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiftingButton : MonoBehaviour
{
    public PresentType mypresent;
    public TextMeshProUGUI amounttxt;
    private int amount=0;
    private ConfidantMenu cm;
    public AudioClip nogifts;

    // Start is called before the first frame update
    private void Start()
    {
        DataManager.instance.giftAcquired.AddListener(Updateamt);
        cm = GameObject.FindObjectOfType<ConfidantMenu>();
    }

    public void GivePresent()
    {
        if (DataManager.instance.SubtractGift(mypresent, 1))
        {
            cm.GiveGift(mypresent);
            
        }
        else
        {
            DataManager.instance.am.PlaySound(nogifts);
        }
        
    }

    public void Updateamt()
    {
        amount = DataManager.instance.GetGiftCount(mypresent);
        amounttxt.text = "x" + amount;

        
    }

  
}
