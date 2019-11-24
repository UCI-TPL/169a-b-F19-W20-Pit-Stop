using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class InventoryPanel : MonoBehaviour
{

    //private PlayerEntity playerScript;

    [SerializeField] GameObject inventory;

    [SerializeField] TextMeshProUGUI giftOneText;
    [SerializeField] TextMeshProUGUI giftTwoText;
    [SerializeField] TextMeshProUGUI giftThreeText;
    [SerializeField] TextMeshProUGUI giftFourText;
    [SerializeField] TextMeshProUGUI giftFiveText;
    [SerializeField] TextMeshProUGUI carPartsText;

   

    // Start is called before the first frame update
    void Start()
    {
        DataManager.instance.carPartAcquired.AddListener(OnPartAcquired);
        DataManager.instance.giftAcquired.AddListener(OnGiftAcquired);
        OnPartAcquired();
        OnGiftAcquired();
        inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            Display();
        }
        else
        {
            Close();
        }
    }

    void OnPartAcquired()
    {
        Debug.Log("Increase car parts");
        carPartsText.text = string.Format("x {0}",DataManager.instance.GetCarParts());
    }

    void OnGiftAcquired()
    {
        Debug.Log("Increase gifts");
        giftOneText.text = string.Format("x {0}", DataManager.instance.GetGiftCount(PresentType.one));
        giftTwoText.text = string.Format("x {0}", DataManager.instance.GetGiftCount(PresentType.two));
        giftThreeText.text = string.Format("x {0}", DataManager.instance.GetGiftCount(PresentType.three));
        giftFourText.text = string.Format("x {0}", DataManager.instance.GetGiftCount(PresentType.four));
        giftFiveText.text = string.Format("x {0}", DataManager.instance.GetGiftCount(PresentType.five));
    }

    public void Display()
    {
        inventory.SetActive(true);
    }

    public void Close()
    {
        inventory.SetActive(false);
    }
}
