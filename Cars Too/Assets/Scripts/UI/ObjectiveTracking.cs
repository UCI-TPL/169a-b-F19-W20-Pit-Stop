using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveTracking : MonoBehaviour
{
    private TextMeshProUGUI objtext = null;

    [SerializeField] private GameObject objcomplete = null;
    // Start is called before the first frame update
    void Start()
    {
        objtext = GetComponent<TextMeshProUGUI>();
        DataManager.instance.carPartAcquired.AddListener(UpdateQuest);
        UpdateQuest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateQuest()
    {
        objtext.text = "Gather Parts: " + DataManager.instance.carParts + "/"+ DataManager.instance.partsneeded;
        if (DataManager.instance.carParts >= DataManager.instance.partsneeded)
        {
            objtext.fontStyle = FontStyles.Strikethrough| FontStyles.Bold;
            objcomplete.SetActive(true);
        }
    }

    public void SetPartsNeeded(int n)
    {
        DataManager.instance.partsneeded = n;
        UpdateQuest();
        objtext.fontStyle =FontStyles.Bold;
        objcomplete.SetActive(false);
    }
}
