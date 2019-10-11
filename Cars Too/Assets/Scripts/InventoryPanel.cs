using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class InventoryPanel : MonoBehaviour
{

    private GameObject player;
    private PlayerEntity playerScript;

    [SerializeField] GameObject inventory;

    [SerializeField] TextMeshProUGUI giftOneText;
    [SerializeField] TextMeshProUGUI giftTwoText;
    [SerializeField] TextMeshProUGUI giftThreeText;
    [SerializeField] TextMeshProUGUI giftFourText;
    [SerializeField] TextMeshProUGUI giftFiveText;
    [SerializeField] TextMeshProUGUI carPartsText;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerEntity>();

        playerScript.carPartAcquired.AddListener(OnPartAcquired);
        playerScript.giftAcquired.AddListener(OnGiftAcquired);
        
    }

    // Start is called before the first frame update
    void Start()
    {
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
        carPartsText.text = string.Format("x {0}", playerScript.GetCarParts());
    }

    void OnGiftAcquired()
    {
        Debug.Log("Increase gifts");
        giftOneText.text = string.Format("x {0}", playerScript.GetGiftCount(PresentType.one));
        giftTwoText.text = string.Format("x {0}", playerScript.GetGiftCount(PresentType.two));
        giftThreeText.text = string.Format("x {0}", playerScript.GetGiftCount(PresentType.three));
        giftFourText.text = string.Format("x {0}", playerScript.GetGiftCount(PresentType.four));
        giftFiveText.text = string.Format("x {0}", playerScript.GetGiftCount(PresentType.five));
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
